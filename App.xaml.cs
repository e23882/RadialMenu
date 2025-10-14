using System.Windows;

namespace RadialMenu
{
    public partial class App : System.Windows.Application
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private RadialWindow _radialWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the shutdown mode to explicit, so the app doesn't close prematurely.
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Initialize the radial window but don't show it yet.
            _radialWindow = new RadialWindow();

            // Initialize the NotifyIcon (system tray icon).
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "RadialMenu";

            // Create the context menu for the tray icon.
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, OnExitClicked);

            // Subscribe to the mouse hook event and start the hook.
            LowLevelMouseHook.MiddleMouseClicked += OnMiddleMouseClicked;
            LowLevelMouseHook.Start();
        }

        private void OnMiddleMouseClicked(object sender, System.Windows.Point e)
        {
            // When the middle mouse button is clicked, show the radial window at the cursor's position.
            _radialWindow.ShowAt(e.X, e.Y);
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            // Shutdown the application when "Exit" is clicked.
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Cleanup resources on exit.
            LowLevelMouseHook.Stop();
            _notifyIcon.Dispose();

            base.OnExit(e);
        }
    }
}
