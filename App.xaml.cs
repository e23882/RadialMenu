using Autofac;
using RadialMenu.ViewModel;
using System.Windows;

namespace RadialMenu
{
    public partial class App : System.Windows.Application
    {
        #region Fields
        private NotifyIcon _notifyIcon;
        private RadialWindow _radialWindow;
        #endregion

        #region Properties
        public static IContainer Container { get; set; }
        #endregion

        #region MemberFunction
        public App()
        {
            InitialDIContainer();
        }

        public void InitialDIContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<RadialWindowViewModel>()
                .SingleInstance();
            builder.RegisterType<SettingsWindowViewModel>()
                .SingleInstance();

            Container = builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            _radialWindow = new RadialWindow();

            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = SystemIcons.Application;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "RadialMenu";

            _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
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
            var _settingsWindow = new SettingsWindow();
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
        #endregion
    }
}
