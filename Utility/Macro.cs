using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; // For Key enum

namespace RadialMenu.Utility
{
    public enum MacroActionType
    {
        KeyPress,
        KeyRelease,
        MouseLeftPress,
        MouseLeftRelease,
        MouseRightPress,
        MouseRightRelease,
        Delay,
        WindowCommand
    }

    public class MacroStep : INotifyPropertyChanged
    {
        private MacroActionType _actionType;
        public MacroActionType ActionType
        {
            get => _actionType;
            set
            {
                if (_actionType != value)
                {
                    _actionType = value;
                    OnPropertyChanged(nameof(ActionType));
                }
            }
        }

        private Key _key;
        public Key Key
        {
            get => _key;
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged(nameof(Key));
                }
            }
        }

        private int _delayMilliseconds;
        public int DelayMilliseconds
        {
            get => _delayMilliseconds;
            set
            {
                if (_delayMilliseconds != value)
                {
                    _delayMilliseconds = value;
                    OnPropertyChanged(nameof(DelayMilliseconds));
                }
            }
        }

        private string _script;
        public string Script
        {
            get => _script;
            set
            {
                if (_script != value)
                {
                    _script = value;
                    OnPropertyChanged(nameof(Script));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Macro
    {
        public List<MacroStep> Steps { get; set; } = new List<MacroStep>();
    }
}
