using System.Diagnostics;
using System.Runtime.Serialization;

namespace GERLIE_WPF.Components
{
    [DataContract]
    public class Class_for_component : View_model_base
    {
        [DataMember]
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
