using RadialMenu.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadialMenu
{
    public class RadialWindowViewModel:ViewModelBase
    {
        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ICommand TopButtonClickCommand { get; set; }
        private void TopButtonClickCommandAction(object? obj)
        {
            MessageBox.Show("Top");
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand RightButtonClickCommand { get; set; }
        private void RightButtonClickCommandAction(object? obj)
        {
            MessageBox.Show("Right");
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ButtonSideButtonClickCommand { get; set; }
        private void ButtonSideButtonClickCommandAction(object? obj)
        {
            MessageBox.Show("Button");
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand LeftButtonClickCommand { get; set; }
        private void LeftButtonClickCommandAction(object? obj)
        {
            MessageBox.Show("Left");
        }
        #endregion

        #region MemberFunction
        public RadialWindowViewModel()
        {
            InitialCommand();
        }

        private void InitialCommand()
        {
            TopButtonClickCommand = new RelayCommand(TopButtonClickCommandAction);
            RightButtonClickCommand = new RelayCommand(RightButtonClickCommandAction);
            ButtonSideButtonClickCommand = new RelayCommand(ButtonSideButtonClickCommandAction);
            LeftButtonClickCommand = new RelayCommand(LeftButtonClickCommandAction);
        }

        #endregion
    }
}
