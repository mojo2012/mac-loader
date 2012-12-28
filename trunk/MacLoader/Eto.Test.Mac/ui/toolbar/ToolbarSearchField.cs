using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Drawing;

namespace Eto.MacLoader.UI {
    public class ToolbarSearchField : NSToolbarItem {
        public delegate void ClickedEventHandler(object sender,EventArgs e);

        public delegate void ChangedEventHandler(object sender,EventArgs e);

        public event ClickedEventHandler Clicked;
        public event ChangedEventHandler Changed;

        public ToolbarSearchField(String title, RectangleF size, String identifier) : base(identifier) {
            var view = new NSSearchField(size);
            this.View = view;
            this.Label = title;

            view.Activated += clicked;
            view.Changed += changed;
        }

        public ToolbarSearchField(String title, String identifier) : this(title, new RectangleF(0f, 0f, 200f, 23f), identifier) {
        }

        public ToolbarSearchField(String title) : this(title, title.ToLower().Replace(" ", "_")) {
        }

        private void clicked(object sender, EventArgs e) {
            if (Clicked != null)
                Clicked(sender, e);
        }

        private void changed(object sender, EventArgs e) {
            if (Changed != null)
                Changed(sender, e);
        }
    }
}