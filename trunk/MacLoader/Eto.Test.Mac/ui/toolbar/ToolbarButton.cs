using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Drawing;

namespace Eto.MacLoader.UI {
    public class ToolbarButton : NSToolbarItem {
        public ToolbarButton(String text, NSImage icon, RectangleF size, String identifier) : base(identifier) {
            this.View = new NSButton(size) { Title = text, BezelStyle = NSBezelStyle.TexturedRounded };
            this.Label = text;

            if (icon != null) {
                this.Image = icon;
            }
        }

        public ToolbarButton(String title, NSImage icon) : this(title, icon, new RectangleF(0f, 0f, 40f, 23f), title.ToLower().Replace(" ", "_")) {
        }
    }
}

