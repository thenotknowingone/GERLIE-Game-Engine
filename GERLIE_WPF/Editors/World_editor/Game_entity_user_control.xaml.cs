﻿using System.Windows.Controls;

namespace GERLIE_WPF.Editors
{
    public partial class Game_entity_user_control : UserControl
    {
        public static Game_entity_user_control Instance 
        { 
            get; 
            private set;  
        }
        public Game_entity_user_control()
        {
            InitializeComponent();
            DataContext = null;
            Instance = this;
        }
    }
}
