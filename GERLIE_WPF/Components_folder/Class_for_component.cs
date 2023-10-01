using System.Diagnostics;

namespace GERLIE_WPF.Components
{
    public class Class_for_component : View_model_base
    { 
        public Class_for_game_entity Owner
        {
            get;
            private set;
        }
        public Class_for_component(Class_for_game_entity owner)
        {
            Debug.Assert(owner != null);
            Owner = owner;
        }
    }
}
