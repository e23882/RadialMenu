using Autofac;
using RadialMenu.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace RadialMenu
{
    public partial class SettingsWindow : Window
    {
        #region Properties
        public SettingsWindowViewModel ViewModel { get; set; }
        #endregion

        #region Public Methods
        public SettingsWindow()
        { 
            InitializeComponent();
            ViewModel = App.Container.Resolve<SettingsWindowViewModel>();
            this.DataContext = ViewModel;
            this.KeyDown += SettingsWindow_KeyDown;
        }

        private void SettingsWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
        }
        #endregion
    }
}