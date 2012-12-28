using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader {
    public class ToolbarSearchField : NSToolbarItem {
        public ToolbarSearchField(String title, String identifier) : base(identifier) {
            this.View = new NSSearchField();
            this.Label = title;
        }

        public ToolbarSearchField(String title) : this(title, NSToolbar.NSToolbarCustomizeToolbarItemIdentifier) {
        }
    }
}