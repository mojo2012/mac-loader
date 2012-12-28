using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using System.Drawing;

namespace Eto.MacLoader.UI {
    public class ToolbarSearchField : NSToolbarItem {
        public ToolbarSearchField(String title, RectangleF size, String identifier) : base(identifier) {
            this.View = new NSSearchField(size);
            this.Label = title;
        }

        public ToolbarSearchField(String title, String identifier) : this(title, new RectangleF(0f, 0f, 200f, 23f), identifier) {
        }

        public ToolbarSearchField(String title) : this(title, title.ToLower().Replace(" ", "_")) {
        }
    }
}