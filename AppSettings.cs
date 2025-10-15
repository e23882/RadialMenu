using System.Collections.Generic;
using System.Windows.Media;
using RadialMenu.Utility;

namespace RadialMenu
{
    public class AppSettings
    {
        public bool MouseMenuEnable { get; set; }
        public string SelectedKey { get; set; }
        public string Button1Text { get; set; }
        public string Button2Text { get; set; }
        public string Button3Text { get; set; }
        public string Button4Text { get; set; }
        public double PanelOpacity { get; set; }
        public string PanelColor { get; set; } = "Gray";
        public double FontSize { get; set; } = 12; // Default font size
        public Dictionary<int, Macro> ButtonMacros { get; set; } = new Dictionary<int, Macro>();
    }
}