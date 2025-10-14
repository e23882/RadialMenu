using RadialMenu.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            var settings = new { MouseMenuEnable, SelectedKey };
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
        public SettingsWindowViewModel()
        {
            InitialCommand();
            InitialSelection();
            LoadSettings();
            SelectedKey = string.Empty;
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
            }
        }

        private void InitialSelection()
        {
            var allKeys = new List<object>();

            var keyboardKeys = Enum.GetValues(typeof(Key)).Cast<Key>();
            allKeys.AddRange(keyboardKeys.Cast<object>());

            // 3. 取得所有滑鼠按鍵 (MouseButton enum) 並加入列表
            var mouseButtons = Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>();
            allKeys.AddRange(mouseButtons.Cast<object>());
            foreach(var item in allKeys)
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
