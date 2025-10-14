using Autofac;
using System.Windows;

namespace RadialMenu
{
    public partial class RadialWindow : Window
    {
        #region Properties
        public RadialWindowViewModel ViewModel { get; set; }
        #endregion

        #region Public Methods
        public RadialWindow()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<RadialWindowViewModel>();
            this.DataContext = ViewModel;
        }
        private void RootLayout_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Shows the window centered at the specified screen coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate of the center point.</param>
        /// <param name="y">The y-coordinate of the center point.</param>
        public void ShowAt(double x, double y)
        {
            this.Left = x - (this.Width / 2);
            this.Top = y - (this.Height / 2);
            this.Show();
            this.Activate();
        }
        #endregion
    }
}
