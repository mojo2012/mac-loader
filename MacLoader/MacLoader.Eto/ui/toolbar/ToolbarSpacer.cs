using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace MacLoader.UI {
    public class ToolbarSpacer : NSToolbarItem {
        public ToolbarSpacer() : base(NSToolbar.NSToolbarSpaceItemIdentifier) {
        }
    }
}
