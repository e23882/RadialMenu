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
    public class RadialWindowViewModel:ViewModelBase
    {
        #region Fields
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
            Button1Text = "Button 1";
            Button2Text = "Button 2";
            Button3Text = "Button 3";
            Button4Text = "Button 4";
            PanelOpacity = 0.8;
            PanelColor = System.Windows.Media.Brushes.Gray;
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
