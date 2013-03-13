using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Drawing;

namespace MacLoader.UI {
    public class ToolbarButton : NSToolbarItem {
        public delegate void ClickedEventHandler(object sender,EventArgs e);

        public event ClickedEventHandler Clicked;
        public event ClickedEventHandler MouseUp;

        public ToolbarButton(String text, NSImage icon, RectangleF size, String identifier) : base(identifier) {
            var button = new Button(size) { Title = text, BezelStyle = NSBezelStyle.TexturedRounded };
            this.View = button;
            this.Label = text;

            button.Activated += buttonClicked;
//            button.MouseUp += buttonMouseUp;

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

        private void buttonMouseUp(object sender, EventArgs e) {
            if (MouseUp != null)
                MouseUp(sender, e);
        }

        public NSView ButtonView {
            get { return this.View; }
        }

        private class Button : NSButton {
            public Button(RectangleF size) : base(size) {
            }

//            public override void MouseUp(NSEvent e) {
//                if (e.Type == NSEventType.LeftMouseUp)
//                     System.Console.Write("test");
//            }
        }
    }
}

