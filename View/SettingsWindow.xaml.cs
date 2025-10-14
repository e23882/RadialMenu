using Autofac;
using RadialMenu.ViewModel;
using System.ComponentModel;
using System.Windows;

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
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
        }
        #endregion
    }
}
