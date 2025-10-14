using System;
using System.Windows;

namespace RadialMenu
{
    public partial class App : System.Windows.Application
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private RadialWindow _radialWindow;
        private SettingsWindow _settingsWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            _radialWindow = new RadialWindow();
            _settingsWindow = new SettingsWindow();

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "RadialMenu";

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Settings", null, OnSettingsClicked);
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, OnExitClicked);

            LowLevelMouseHook.MiddleMouseClicked += OnMiddleMouseClicked;
            LowLevelMouseHook.Start();
        }

        private void OnMiddleMouseClicked(object sender, System.Windows.Point e)
        {
            _radialWindow.ShowAt(e.X, e.Y);
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            _settingsWindow.Show();
            _settingsWindow.Activate();
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LowLevelMouseHook.Stop();
            _notifyIcon.Dispose();

            base.OnExit(e);
        }
    }
}
