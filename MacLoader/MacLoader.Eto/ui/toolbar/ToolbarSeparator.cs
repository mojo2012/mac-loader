using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader.UI {
    public class ToolbarSeparator : NSToolbarItem {
        public ToolbarSeparator() : base(NSToolbar.NSToolbarSeparatorItemIdentifier) {
        }
    }
}
