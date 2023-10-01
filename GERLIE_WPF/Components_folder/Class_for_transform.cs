using GERLIE_WPF.Components;
using System.Runtime.Serialization;
using System.Numerics;

namespace GERLIE_WPF.Components_folder
{
    [DataContract]
    public class Class_for_transform : Class_for_component
    {
        private Class_for_vector_3 _position;
        public Class_for_vector_3 Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        private Class_for_vector_3 _rotation;
        public Class_for_vector_3 Rotation
        {
            get => _rotation;
            set
            {
                if (_rotation != value)
                {
                    _rotation = value;
                    OnPropertyChanged(nameof(Rotation));
                }
            }
        }

        private Class_for_vector_3 _scale;
        public Class_for_vector_3 Scale
        {
            get => _scale;
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    OnPropertyChanged(nameof(Scale));
                }
            }
        }
        public int 
        public Class_for_transform(Class_for_game_entity owner) : base(owner)
        {
        }
    }
}
