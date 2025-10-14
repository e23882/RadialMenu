using RadialMenu.Utility;
using System.Collections.ObjectModel;
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
        public RelayCommand MouseMenuKeyDownCommand { get; set; }
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
        }
        #endregion
    }
}
