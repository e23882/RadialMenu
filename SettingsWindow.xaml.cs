using System.ComponentModel;
using System.Windows;

namespace RadialMenu
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Cancel the closing event
            e.Cancel = true;
            // Hide the window instead of closing it
            this.Hide();
        }
    }
}
