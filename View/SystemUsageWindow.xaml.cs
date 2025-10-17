
using System.Windows;
using System.Windows.Input;
using System;

namespace RadialMenu.View
{
    public partial class SystemUsageWindow : Window
    {
        public event EventHandler<System.Windows.Point> PositionChanged;

        public SystemUsageWindow()
        {
            InitializeComponent();
            this.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object sender, EventArgs e)
        {
            PositionChanged?.Invoke(this, new System.Windows.Point(this.Left, this.Top));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
