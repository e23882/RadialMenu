using RadialMenu.Utility;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;

namespace RadialMenu.ViewModel
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<string> _AllKeyCollection = new ObservableCollection<string>();
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
        public RelayCommand MouseMenuKeyDownCommand { get; set; }

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
            var settings = new AppSettings
            {
                MouseMenuEnable = this.MouseMenuEnable,
                SelectedKey = this.SelectedKey,
                Button1Text = this.Button1Text,
                Button2Text = this.Button2Text,
                Button3Text = this.Button3Text,
                Button4Text = this.Button4Text,
                PanelOpacity = this.PanelOpacity,
                PanelColor = this.PanelColor,
                FontSize = this.FontSize
            };
            string jsonString = JsonSerializer.Serialize(settings);
            File.WriteAllText("settings.json", jsonString);

            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void MouseMenuKeyDownCommandAction(object obj)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand MouseMenuKeyUpCommand { get; set; }
        private void MouseMenuKeyUpCommandAction(object obj)
        {
        }
        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        public SettingsWindowViewModel()
        {
            InitialCommand();
            InitialSelection();
            Button1Text = "Button 1";
            Button2Text = "Button 2";
            Button3Text = "Button 3";
            Button4Text = "Button 4";
            PanelOpacity = 0.8;
            PanelColor = "Gray";
            FontSize = 12; // Default value
            ColorOptions = new ObservableCollection<string> { "Gray", "Black", "White", "Red", "Green", "Blue" };
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string jsonString = File.ReadAllText("settings.json");
                AppSettings settings = JsonSerializer.Deserialize<AppSettings>(jsonString);
                MouseMenuEnable = settings.MouseMenuEnable;
                SelectedKey = settings.SelectedKey;
                Button1Text = settings.Button1Text;
                Button2Text = settings.Button2Text;
                Button3Text = settings.Button3Text;
                Button4Text = settings.Button4Text;
                PanelOpacity = settings.PanelOpacity;
                PanelColor = settings.PanelColor;
                FontSize = settings.FontSize;
            }
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
            MouseMenuKeyDownCommand = new RelayCommand(MouseMenuKeyDownCommandAction);
            MouseMenuKeyUpCommand = new RelayCommand(MouseMenuKeyUpCommandAction);
            SaveSettingCommand = new RelayCommand(SaveSettingCommandAction);
            CancelCommand = new RelayCommand(CancelCommandAction);
            ChooseColorCommand = new RelayCommand(ChooseColorCommandAction);
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
