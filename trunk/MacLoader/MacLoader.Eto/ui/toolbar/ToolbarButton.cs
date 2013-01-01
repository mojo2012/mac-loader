using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Drawing;

namespace Eto.MacLoader.UI {
    public class ToolbarButton : NSToolbarItem {
        public delegate void ClickedEventHandler(object sender,EventArgs e);

        public event ClickedEventHandler Clicked;

        public ToolbarButton(String text, NSImage icon, RectangleF size, String identifier) : base(identifier) {
            var button = new NSButton(size) { Title = text, BezelStyle = NSBezelStyle.TexturedRounded };
            this.View = button;
            this.Label = text;

            button.Activated += buttonClicked;

            if (icon != null) {
                this.Image = icon;
            }
        }

        public ToolbarButton(String title, NSImage icon) : this(title, icon, new RectangleF(0f, 0f, 40f, 23f), title.ToLower().Replace(" ", "_")) {
        }

        private void buttonClicked(object sender, EventArgs e) {
            if (Clicked != null)
                Clicked(sender, e);
        }

        public NSView ButtonView {
            get { return this.View; }
        }
    }
}

