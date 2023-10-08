using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERLIE_WPF.Utilities
{
    public interface IUndoRedo
    {
        string Name
        {
            get;
        }
        void Undo();
        void Redo();
    }
    public class Class_for_undo_redo_action: IUndoRedo
    {
        private Action _undo_action;
        private Action _redo_action;
        public string Name
        {
            get;
        }
        public void Redo() => _redo_action();
        public void Undo() => _undo_action();
        public Class_for_undo_redo_action(string name)
        {
            Name = name;
        }
        public Class_for_undo_redo_action(Action undo, Action redo, string name) :this(name)
        { 
            Debug.Assert(undo != null && redo != null);
            _undo_action = undo;
            _redo_action = redo;
        }

        public Class_for_undo_redo_action(string property, object instance, object undo_value, object redo_value, string name) : 
            this
                (
                    () => instance.GetType().GetProperty(property).SetValue(instance, undo_value),
                    () => instance.GetType().GetProperty(property).SetValue(instance, redo_value),
                    name
                )
        {
        }
    }
    public class Class_for_undo_redo
    {
        private bool _enable_add= true;
        private readonly ObservableCollection<IUndoRedo> _undo_list = new ObservableCollection<IUndoRedo>();
        private readonly ObservableCollection<IUndoRedo> _redo_list = new ObservableCollection<IUndoRedo>();
        public ReadOnlyObservableCollection<IUndoRedo> Redo_list
        {
            get;
        }
        public ReadOnlyObservableCollection<IUndoRedo> Undo_list
        {
            get;
        }
        public void Reset()
        {
            _undo_list.Clear();
            _redo_list.Clear();
        }
        public void Add(IUndoRedo cmd)
        {
            if (_enable_add)
            {
                _undo_list.Add(cmd);
                _redo_list.Clear();
            }
        }
        public void Undo()
        {
            if(_undo_list.Any())                                                                                //Checks if there is anything to undo.
            {
                var cmd = _undo_list.Last();                                                                    //Picks the last object added to the undo list.
                _undo_list.RemoveAt(_undo_list.Count - 1);                                                      //Removes last object added.
                _enable_add = false;
                cmd.Undo();                                                                                     //Calls Undo method from IUndoRedo interface.
                _enable_add = true;
                _redo_list.Insert(0, cmd);                                                                      //Adds the undone object to the redo list.
            }
        }
        public void Redo()
        {
            if (_redo_list.Any())                                                                               
            {
                var cmd = _redo_list.First();                                                                   
                _redo_list.RemoveAt(0);
                _enable_add = false;
                cmd.Redo();
                _enable_add = true;
                _undo_list.Add(cmd);                                                                            
            }
        }
        public Class_for_undo_redo()
        {
            Redo_list = new ReadOnlyObservableCollection<IUndoRedo>(_redo_list);
            Undo_list = new ReadOnlyObservableCollection<IUndoRedo>(_undo_list);
        }
    }
}
