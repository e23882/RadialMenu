using Autofac;
using RadialMenu.ViewModel;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
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

            UpdateHook();

            LowLevelMouseHook.Start();
        }

        private void UpdateHook()
        {
            // Clear existing hooks
            LowLevelMouseHook.LeftMouseButtonClicked -= OnMouseButtonClicked;
            LowLevelMouseHook.RightMouseButtonClicked -= OnMouseButtonClicked;
            LowLevelMouseHook.MiddleMouseButtonClicked -= OnMouseButtonClicked;

            if (File.Exists("settings.json"))
            {
                string jsonString = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<JsonElement>(jsonString);
                var radialMenuViewModel = Container.Resolve<RadialWindowViewModel>();

                bool mouseMenuEnabled = false;
                if (settings.TryGetProperty("MouseMenuEnable", out var mouseMenuEnableElement))
                {
                    mouseMenuEnabled = mouseMenuEnableElement.GetBoolean();
                }

                if (mouseMenuEnabled)
                {
                    if (settings.TryGetProperty("SelectedKey", out var selectedKeyElement))
                    {
                        string selectedKey = selectedKeyElement.GetString();
                        switch (selectedKey)
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
                        }
                    }
                }

                if (settings.TryGetProperty("Button1Text", out var button1TextElement))
                {
                    radialMenuViewModel.Button1Text = button1TextElement.GetString();
                }
                if (settings.TryGetProperty("Button2Text", out var button2TextElement))
                {
                    radialMenuViewModel.Button2Text = button2TextElement.GetString();
                }
                if (settings.TryGetProperty("Button3Text", out var button3TextElement))
                {
                    radialMenuViewModel.Button3Text = button3TextElement.GetString();
                }
                if (settings.TryGetProperty("Button4Text", out var button4TextElement))
                {
                    radialMenuViewModel.Button4Text = button4TextElement.GetString();
                }
                if (settings.TryGetProperty("PanelOpacity", out var panelOpacityElement))
                {
                    radialMenuViewModel.PanelOpacity = panelOpacityElement.GetDouble();
                }
                if (settings.TryGetProperty("PanelColor", out var panelColorElement))
                {
                    var color = (SolidColorBrush)new BrushConverter().ConvertFromString(panelColorElement.GetString());
                    radialMenuViewModel.PanelColor = color;
                }
            }
        }

        private void OnMouseButtonClicked(object sender, System.Windows.Point e)
        {
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

            base.OnExit(e);
        }
        #endregion
    }
}
