using RadialMenu.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace RadialMenu.ViewModel
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<string> _AllKeyCollection = new ObservableCollection<string>();
        private AppSettings _appSettings; // Declare AppSettings field
        #endregion

        #region Properties
        public ObservableCollection<string> AllKeyCollection
        {
            get
            {
                return _AllKeyCollection;
            }
            set
            {
                _AllKeyCollection = value;
                OnPropertyChanged();
            }
        }
        private bool _mouseMenuEnable;
        public bool MouseMenuEnable
        {
            get { return _mouseMenuEnable; }
            set
            {
                _mouseMenuEnable = value;
                OnPropertyChanged();
            }
        }

        private string _selectedKey;
        public string SelectedKey
        {
            get { return _selectedKey; }
            set
            {
                _selectedKey = value;
                OnPropertyChanged();
            }
        }

        private string _button1Text;
        public string Button1Text
        {
            get { return _button1Text; }
            set
            {
                _button1Text = value;
                OnPropertyChanged();
            }
        }

        private string _button2Text;
        public string Button2Text
        {
            get { return _button2Text; }
            set
            {
                _button2Text = value;
                OnPropertyChanged();
            }
        }

        private string _button3Text;
        public string Button3Text
        {
            get { return _button3Text; }
            set
            {
                _button3Text = value;
                OnPropertyChanged();
            }
        }

        private string _button4Text;
        public string Button4Text
        {
            get { return _button4Text; }
            set
            {
                _button4Text = value;
                OnPropertyChanged();
            }
        }

        private double _panelOpacity;
        public double PanelOpacity
        {
            get { return _panelOpacity; }
            set
            {
                _panelOpacity = value;
                OnPropertyChanged();
            }
        }

        private string _panelColor;
        public string PanelColor
        {
            get { return _panelColor; }
            set
            {
                _panelColor = value;
                OnPropertyChanged();
            }
        }

        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> ColorOptions { get; set; }
        public ICommand ChooseColorCommand { get; set; }

        private int _selectedButtonIndex;
        public int SelectedButtonIndex
        {
            get => _selectedButtonIndex;
            set
            {
                if (_selectedButtonIndex != value)
                {
                    _selectedButtonIndex = value;
                    OnPropertyChanged();
                    LoadMacroForSelectedButton();
                }
            }
        }

        private ObservableCollection<MacroStep> _currentMacroSteps;
        public ObservableCollection<MacroStep> CurrentMacroSteps
        {
            get => _currentMacroSteps;
            set
            {
                _currentMacroSteps = value;
                OnPropertyChanged();
            }
        }

        private MacroStep _selectedMacroStep;
        public MacroStep SelectedMacroStep
        {
            get => _selectedMacroStep;
            set
            {
                _selectedMacroStep = value;
                OnPropertyChanged();
            }
        }

        private MacroActionType _newMacroActionType;
        public MacroActionType NewMacroActionType
        {
            get => _newMacroActionType;
            set
            {
                _newMacroActionType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanAddMacroStep));
            }
        }

        private Key _newMacroKey;
        public Key NewMacroKey
        {
            get => _newMacroKey;
            set
            {
                _newMacroKey = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanAddMacroStep));
            }
        }

        private int _newMacroDelayMilliseconds;
        public int NewMacroDelayMilliseconds
        {
            get => _newMacroDelayMilliseconds;
            set
            {
                if (_newMacroDelayMilliseconds != value)
                {
                    _newMacroDelayMilliseconds = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanAddMacroStep));
                }
            }
        }

        public bool CanAddMacroStep
        {
            get
            {
                if (NewMacroActionType == MacroActionType.Delay)
                {
                    return NewMacroDelayMilliseconds >= 0; // Delay must be non-negative
                }
                else
                {
                    return NewMacroKey != Key.None; // For other actions, a key must be selected
                }
            }
        }

        public ObservableCollection<MacroActionType> AllMacroActionTypes { get; set; }
        public ObservableCollection<Key> AllKeys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand CancelCommand { get; set; }
        private void CancelCommandAction(object? obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveSettingCommand { get; set; }
        private void SaveSettingCommandAction(object? obj)
        {
            _appSettings.MouseMenuEnable = this.MouseMenuEnable;
            _appSettings.SelectedKey = this.SelectedKey;
            _appSettings.Button1Text = this.Button1Text;
            _appSettings.Button2Text = this.Button2Text;
            _appSettings.Button3Text = this.Button3Text;
            _appSettings.Button4Text = this.Button4Text;
            _appSettings.PanelOpacity = this.PanelOpacity;
            _appSettings.PanelColor = this.PanelColor;
            _appSettings.FontSize = this.FontSize;

            SettingsManager.SaveSettings(_appSettings);

            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddMacroStepCommand { get; set; }
        public ICommand RemoveMacroStepCommand { get; set; }
        public ICommand SaveMacroCommand { get; set; }

        private void AddMacroStepCommandAction(object? obj)
        {
            if (!CanAddMacroStep)
            {
                return;
            }

            var newStep = new MacroStep
            {
                ActionType = NewMacroActionType,
                Key = NewMacroActionType == MacroActionType.Delay ? Key.None : NewMacroKey, // Key is only relevant for non-delay actions
                DelayMilliseconds = NewMacroActionType == MacroActionType.Delay ? NewMacroDelayMilliseconds : 0
            };

            if (SelectedMacroStep != null)
            {
                int index = CurrentMacroSteps.IndexOf(SelectedMacroStep);
                if (index >= 0)
                {
                    CurrentMacroSteps.Insert(index + 1, newStep);
                }
                else
                {
                    CurrentMacroSteps.Add(newStep);
                }
            }
            else
            {
                CurrentMacroSteps.Add(newStep);
            }
        }

        private void RemoveMacroStepCommandAction(object? obj)
        {
            if (SelectedMacroStep != null)
            {
                CurrentMacroSteps.Remove(SelectedMacroStep);
                SelectedMacroStep = null;
            }
        }

        private void SaveMacroCommandAction(object? obj)
        {
            if (SelectedButtonIndex > 0 && SelectedButtonIndex <= 4)
            {
                if (_appSettings.ButtonMacros.ContainsKey(SelectedButtonIndex))
                {
                    _appSettings.ButtonMacros[SelectedButtonIndex].Steps = CurrentMacroSteps.ToList();
                }
                else
                {
                    _appSettings.ButtonMacros.Add(SelectedButtonIndex, new Macro { Steps = CurrentMacroSteps.ToList() });
                }
            }
            SettingsManager.SaveSettings(_appSettings);

            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }

        private void LoadMacroForSelectedButton()
        {
            if (SelectedButtonIndex > 0 && SelectedButtonIndex <= 4)
            {
                if (_appSettings.ButtonMacros.TryGetValue(SelectedButtonIndex, out Macro macro))
                {
                    CurrentMacroSteps = new ObservableCollection<MacroStep>(macro.Steps);
                }
                else
                {
                    CurrentMacroSteps = new ObservableCollection<MacroStep>();
                }
            }
            else
            {
                CurrentMacroSteps = new ObservableCollection<MacroStep>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SettingsWindowViewModel()
        {
            _appSettings = SettingsManager.LoadSettings(); // Initialize AppSettings using SettingsManager
            InitialCommand();
            InitialSelection();
            Button1Text = _appSettings.Button1Text;
            Button2Text = _appSettings.Button2Text;
            Button3Text = _appSettings.Button3Text;
            Button4Text = _appSettings.Button4Text;
            PanelOpacity = _appSettings.PanelOpacity;
            PanelColor = _appSettings.PanelColor;
            FontSize = _appSettings.FontSize;
            ColorOptions = new ObservableCollection<string> { "Gray", "Black", "White", "Red", "Green", "Blue" };

            AllMacroActionTypes = new ObservableCollection<MacroActionType>(Enum.GetValues(typeof(MacroActionType)).Cast<MacroActionType>());
            AllKeys = new ObservableCollection<Key>(Enum.GetValues(typeof(Key)).Cast<Key>());
            NewMacroActionType = MacroActionType.KeyPress;
            NewMacroKey = Key.None;
            NewMacroDelayMilliseconds = 0;
            CurrentMacroSteps = new ObservableCollection<MacroStep>();
            SelectedButtonIndex = 1; // Default to button 1

            LoadSettings();
        }

        private void LoadSettings()
        {
            _appSettings = SettingsManager.LoadSettings();
            MouseMenuEnable = _appSettings.MouseMenuEnable;
            SelectedKey = _appSettings.SelectedKey;
            Button1Text = _appSettings.Button1Text;
            Button2Text = _appSettings.Button2Text;
            Button3Text = _appSettings.Button3Text;
            Button4Text = _appSettings.Button4Text;
            PanelOpacity = _appSettings.PanelOpacity;
            PanelColor = _appSettings.PanelColor;
            FontSize = _appSettings.FontSize;

            // Load macros for the initially selected button
            LoadMacroForSelectedButton();
        }

        private void InitialSelection()
        {
            var mouseButtons = Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>();
            foreach(var item in mouseButtons)
            {
                AllKeyCollection.Add(item.ToString());
            }
            
        }

        private void InitialCommand()
        {
            SaveSettingCommand = new RelayCommand(SaveSettingCommandAction);
            CancelCommand = new RelayCommand(CancelCommandAction);
            ChooseColorCommand = new RelayCommand(ChooseColorCommandAction);
            AddMacroStepCommand = new RelayCommand(AddMacroStepCommandAction, _ => CanAddMacroStep);
            RemoveMacroStepCommand = new RelayCommand(RemoveMacroStepCommandAction);
            SaveMacroCommand = new RelayCommand(SaveMacroCommandAction);
        }

        private void ChooseColorCommandAction(object? obj)
        {
            var colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PanelColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
            }
        }
        #endregion
    }
}
