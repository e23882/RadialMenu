using RadialMenu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace RadialMenu
{
    public class RadialWindowViewModel : ViewModelBase
    {
        #region Fields
        private AppSettings _appSettings; // Declare AppSettings field
        #endregion

        #region Properties
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

        private double _button1ContentRotationAngle;
        public double Button1ContentRotationAngle
        {
            get { return _button1ContentRotationAngle; }
            set
            {
                _button1ContentRotationAngle = value;
                OnPropertyChanged();
            }
        }

        private double _button3ContentRotationAngle;
        public double Button3ContentRotationAngle
        {
            get { return _button3ContentRotationAngle; }
            set
            {
                _button3ContentRotationAngle = value;
                OnPropertyChanged();
            }
        }

        private double _button4ContentRotationAngle;
        public double Button4ContentRotationAngle
        {
            get { return _button4ContentRotationAngle; }
            set
            {
                _button4ContentRotationAngle = value;
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

        private System.Windows.Media.Brush _panelColor;
        public System.Windows.Media.Brush PanelColor
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

        /// <summary>
        /// 
        /// </summary>
        public ICommand TopButtonClickCommand { get; set; }
        private void TopButtonClickCommandAction(object? obj)
        {
            ExecuteMacroForButton(1);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand RightButtonClickCommand { get; set; }
        private void RightButtonClickCommandAction(object? obj)
        {
            ExecuteMacroForButton(2);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ButtonSideButtonClickCommand { get; set; }
        private void ButtonSideButtonClickCommandAction(object? obj)
        {
            ExecuteMacroForButton(3);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand LeftButtonClickCommand { get; set; }
        private void LeftButtonClickCommandAction(object? obj)
        {
            ExecuteMacroForButton(4);
        }

        private void ExecuteMacroForButton(int buttonIndex)
        {
            if (_appSettings.ButtonMacros.TryGetValue(buttonIndex, out Macro macro))
            {
                foreach (var step in macro.Steps)
                {
                    switch (step.ActionType)
                    {
                        case MacroActionType.KeyPress:
                            InputSimulator.SimulateKeyPress(step.Key);
                            break;
                        case MacroActionType.KeyRelease:
                            InputSimulator.SimulateKeyRelease(step.Key);
                            break;
                        case MacroActionType.MouseLeftPress:
                            InputSimulator.SimulateMouseLeftPress();
                            break;
                        case MacroActionType.MouseLeftRelease:
                            InputSimulator.SimulateMouseLeftRelease();
                            break;
                        case MacroActionType.MouseRightPress:
                            InputSimulator.SimulateMouseRightPress();
                            break;
                        case MacroActionType.MouseRightRelease:
                            InputSimulator.SimulateMouseRightRelease();
                            break;
                        case MacroActionType.Delay:
                            InputSimulator.SimulateDelay(step.DelayMilliseconds);
                            break;
                    }
                }
            }
        }
        #endregion

        #region MemberFunction
        private void InitialCommand()
        {
            TopButtonClickCommand = new RelayCommand(TopButtonClickCommandAction);
            RightButtonClickCommand = new RelayCommand(RightButtonClickCommandAction);
            ButtonSideButtonClickCommand = new RelayCommand(ButtonSideButtonClickCommandAction);
            LeftButtonClickCommand = new RelayCommand(LeftButtonClickCommandAction);
        }

        public RadialWindowViewModel()
        {
            _appSettings = SettingsManager.LoadSettings(); // Initialize AppSettings using SettingsManager
            SettingsManager.SettingsChanged += SettingsManager_SettingsChanged; // Subscribe to settings changed event
            InitialCommand();
            Button1Text = "Button 1";
            Button2Text = "Button 2";
            Button3Text = "Button 3";
            Button4Text = "Button 4";
            Button1ContentRotationAngle = 90;
            Button3ContentRotationAngle = -90;
            Button4ContentRotationAngle = 180;
            PanelOpacity = 0.8;
            PanelColor = System.Windows.Media.Brushes.Gray;
            LoadSettings();
        }

        private void SettingsManager_SettingsChanged(object? sender, EventArgs e)
        {
            LoadSettings(); // Reload settings when they change
        }

        private void LoadSettings()
        {
            _appSettings = SettingsManager.LoadSettings();
            PanelOpacity = _appSettings.PanelOpacity;
            PanelColor = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString(_appSettings.PanelColor);
            FontSize = _appSettings.FontSize;
            Button1Text = _appSettings.Button1Text;
            Button2Text = _appSettings.Button2Text;
            Button3Text = _appSettings.Button3Text;
            Button4Text = _appSettings.Button4Text;
        }

        private void InitialSelection()
        {
            var mouseButtons = Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>();
        }

        #endregion
    }
}

