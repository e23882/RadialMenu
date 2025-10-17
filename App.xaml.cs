using Autofac;
using RadialMenu.View;
using RadialMenu.ViewModel;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace RadialMenu
{
    public partial class App : System.Windows.Application
    {
        #region Fields
        private NotifyIcon _notifyIcon;
        private RadialWindow _radialWindow;
        private SystemUsageWindow _systemUsageWindow;
        private SettingsWindowViewModel _settingsViewModel;
        #endregion

        #region Properties
        public bool IsFirstTime { get; set; } = true;
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
            builder.RegisterType<LoadingWindowViewModel>()
                .SingleInstance();
            
            Container = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            await Task.Delay(1500);

            loadingWindow.Close();

            _radialWindow = new RadialWindow();

            _settingsViewModel = Container.Resolve<SettingsWindowViewModel>();
            _settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;

            _systemUsageWindow = new SystemUsageWindow();
            _systemUsageWindow.PositionChanged += SystemUsageWindow_PositionChanged;

            UpdateSystemUsageWindowVisibility();

            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = SystemIcons.Application;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "RadialMenu";

            _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Settings", null, OnSettingsClicked);
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, OnExitClicked);

            UpdateHook();

            LowLevelMouseHook.Start();
        }

        private void UpdateHook()
        {
            // Clear existing hooks
            LowLevelMouseHook.LeftMouseButtonClicked -= OnMouseButtonClicked;
            LowLevelMouseHook.RightMouseButtonClicked -= OnMouseButtonClicked;
            LowLevelMouseHook.MiddleMouseButtonClicked -= OnMouseButtonClicked;
            LowLevelMouseHook.XButton1Clicked -= OnMouseButtonClicked;
            LowLevelMouseHook.XButton2Clicked -= OnMouseButtonClicked;

            AppSettings settings;
            if (File.Exists("settings.json"))
            {
                string jsonString = File.ReadAllText("settings.json");
                settings = JsonSerializer.Deserialize<AppSettings>(jsonString);
            }
            else
            {
                // If settings.json doesn't exist, create and apply default settings
                settings = new AppSettings
                {
                    MouseMenuEnable = true,
                    SelectedKey = "Middle",
                    Button1Text = "Button 1",
                    Button2Text = "Button 2",
                    Button3Text = "Button 3",
                    Button4Text = "SETTING",
                    PanelOpacity = 0.8,
                    PanelColor = "#FF808080",
                    FontSize = 16,
                    ButtonMacros = new System.Collections.Generic.Dictionary<int, Utility.Macro>()
                };
                Utility.SettingsManager.SaveSettings(settings);
            }

            var radialMenuViewModel = Container.Resolve<RadialWindowViewModel>();

            if (settings.MouseMenuEnable)
            {
                switch (settings.SelectedKey)
                {
                    case "Left":
                        LowLevelMouseHook.LeftMouseButtonClicked += OnMouseButtonClicked;
                        break;
                    case "Right":
                        LowLevelMouseHook.RightMouseButtonClicked += OnMouseButtonClicked;
                        break;
                    case "Middle":
                        LowLevelMouseHook.MiddleMouseButtonClicked += OnMouseButtonClicked;
                        break;
                    case "XButton1":
                        LowLevelMouseHook.XButton1Clicked += OnMouseButtonClicked;
                        break;
                    case "XButton2":
                        LowLevelMouseHook.XButton2Clicked += OnMouseButtonClicked;
                        break;
                }
            }

            radialMenuViewModel.Button1Text = settings.Button1Text;
            radialMenuViewModel.Button2Text = settings.Button2Text;
            radialMenuViewModel.Button3Text = settings.Button3Text;
            radialMenuViewModel.Button4Text = settings.Button4Text;
            radialMenuViewModel.PanelOpacity = settings.PanelOpacity;
            try
            {
                radialMenuViewModel.PanelColor = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString(settings.PanelColor);
            }
            catch
            {
                radialMenuViewModel.PanelColor = System.Windows.Media.Brushes.Gray;
            }
            radialMenuViewModel.FontSize = settings.FontSize;
        }

        private void OnMouseButtonClicked(object sender, System.Windows.Point e)
        {
            if (IsFirstTime)
            {
                _radialWindow.ShowAt(e.X, e.Y);
                _radialWindow.Hide();
                _radialWindow.ShowAt(e.X, e.Y);
            }
            else
                _radialWindow.ShowAt(e.X, e.Y);
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            var _settingsWindow = new SettingsWindow();
            _settingsWindow.Closed += (s, args) => UpdateHook();
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
            _systemUsageWindow?.Close();
            base.OnExit(e);
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsWindowViewModel.ShowCPUUseage))
            {
                UpdateSystemUsageWindowVisibility();
            }
        }

        private void UpdateSystemUsageWindowVisibility()
        {
            var settings = Utility.SettingsManager.LoadSettings();
            _systemUsageWindow.Left = settings.SystemUsageWindowLeft;
            _systemUsageWindow.Top = settings.SystemUsageWindowTop;

            if (_settingsViewModel.ShowCPUUseage)
            {
                _systemUsageWindow.Show();
            }
            else
            {
                _systemUsageWindow.Hide();
            }
        }

        private void SystemUsageWindow_PositionChanged(object sender, System.Windows.Point e)
        {
            var settings = Utility.SettingsManager.LoadSettings();
            settings.SystemUsageWindowLeft = e.X;
            settings.SystemUsageWindowTop = e.Y;
            Utility.SettingsManager.SaveSettings(settings);
        }
        #endregion
    }
}
