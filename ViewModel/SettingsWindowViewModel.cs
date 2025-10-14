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
        public bool MouseMenuEnable { get; set; }
        public string SelectedKey { get; set; }
        public string Button1Text { get; set; }
        public string Button2Text { get; set; }
        public string Button3Text { get; set; }
        public string Button4Text { get; set; }
        public double PanelOpacity { get; set; }
        public string PanelColor { get; set; }
        public ObservableCollection<string> ColorOptions { get; set; }
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
            var settings = new 
            {
                MouseMenuEnable,
                SelectedKey,
                Button1Text,
                Button2Text,
                Button3Text,
                Button4Text,
                PanelOpacity,
                PanelColor
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
            ColorOptions = new ObservableCollection<string> { "Gray", "Black", "White", "Red", "Green", "Blue" };
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string jsonString = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<JsonElement>(jsonString);
                if (settings.TryGetProperty("MouseMenuEnable", out var mouseMenuEnableElement))
                {
                    MouseMenuEnable = mouseMenuEnableElement.GetBoolean();
                }
                if (settings.TryGetProperty("SelectedKey", out var selectedKeyElement))
                {
                    SelectedKey = selectedKeyElement.GetString();
                }
                if (settings.TryGetProperty("Button1Text", out var button1TextElement))
                {
                    Button1Text = button1TextElement.GetString();
                }
                if (settings.TryGetProperty("Button2Text", out var button2TextElement))
                {
                    Button2Text = button2TextElement.GetString();
                }
                if (settings.TryGetProperty("Button3Text", out var button3TextElement))
                {
                    Button3Text = button3TextElement.GetString();
                }
                if (settings.TryGetProperty("Button4Text", out var button4TextElement))
                {
                    Button4Text = button4TextElement.GetString();
                }
                if (settings.TryGetProperty("PanelOpacity", out var panelOpacityElement))
                {
                    PanelOpacity = panelOpacityElement.GetDouble();
                }
                if (settings.TryGetProperty("PanelColor", out var panelColorElement))
                {
                    PanelColor = panelColorElement.GetString();
                }
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
        }
        #endregion
    }
}
