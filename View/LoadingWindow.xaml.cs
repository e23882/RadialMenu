using Autofac;
using RadialMenu.ViewModel;
using System.Windows;

namespace RadialMenu.View
{
    public partial class LoadingWindow : Window
    {
        #region Properties
        public LoadingWindowViewModel ViewModel { get; set; }
        #endregion

        #region MemberFunction
        public LoadingWindow()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<LoadingWindowViewModel>();
            this.DataContext = ViewModel;
        }
        #endregion
    }
}
