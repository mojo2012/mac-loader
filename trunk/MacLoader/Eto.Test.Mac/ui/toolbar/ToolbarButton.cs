using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader {
    public class ToolbarButton : NSToolbarItem {
        public ToolbarButton(String text, NSImage icon, String identifier) : base(identifier) {
            this.View = new NSButton(new System.Drawing.RectangleF(0f, 0f, 40f, 23f)) { Title = text, BezelStyle = NSBezelStyle.TexturedRounded };
            this.Label = text;
        }

        public ToolbarButton(String title, NSImage icon) : this(title, icon, NSToolbar.NSToolbarCustomizeToolbarItemIdentifier) {
        }
    }
}

