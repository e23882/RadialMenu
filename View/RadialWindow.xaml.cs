using Autofac;
using System.Windows;
using System.Windows.Media.Animation;

namespace RadialMenu
{
    public partial class RadialWindow : Window
    {
        #region Fields
        private Storyboard _storyboard;
        #endregion

        #region Properties
        public RadialWindowViewModel ViewModel { get; set; }
        #endregion

        #region Public Methods
        public RadialWindow()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<RadialWindowViewModel>();
            ViewModel.CurrentWindow = this;
            this.DataContext = ViewModel;
            CreateStoryboard();
            this.Visibility = Visibility.Hidden; // Ensure window is hidden by default
        }

        private void CreateStoryboard()
        {
            _storyboard = new Storyboard();
            _storyboard.Duration = TimeSpan.FromSeconds(0.01);

            // Button1 (Top)
            var anim1 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(anim1, Button1);
            Storyboard.SetTargetProperty(anim1, new PropertyPath(UIElement.VisibilityProperty));
            anim1.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));

            // Button4 (Left)
            var anim4 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(anim4, Button4);
            Storyboard.SetTargetProperty(anim4, new PropertyPath(UIElement.VisibilityProperty));
            anim4.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0025))));

            // Button3 (Bottom)
            var anim3 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(anim3, Button3);
            Storyboard.SetTargetProperty(anim3, new PropertyPath(UIElement.VisibilityProperty));
            anim3.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.005))));

            // Button2 (Right)
            var anim2 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(anim2, Button2);
            Storyboard.SetTargetProperty(anim2, new PropertyPath(UIElement.VisibilityProperty));
            anim2.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0075))));

            _storyboard.Children.Add(anim1);
            _storyboard.Children.Add(anim2);
            _storyboard.Children.Add(anim3);
            _storyboard.Children.Add(anim4);
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
            Button1.Visibility = Visibility.Collapsed;
            Button2.Visibility = Visibility.Collapsed;
            Button3.Visibility = Visibility.Collapsed;
            Button4.Visibility = Visibility.Collapsed;

            this.Left = x - (this.Width / 2);
            this.Top = y - (this.Height / 2);
            this.Show();
            this.Activate();

            _storyboard.Begin(this);
        }
        #endregion
    }
}
