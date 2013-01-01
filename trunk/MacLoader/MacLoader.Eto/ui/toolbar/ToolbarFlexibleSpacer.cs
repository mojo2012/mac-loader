using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader.UI {
    public class ToolbarFlexibleSpacer : NSToolbarItem {
        public ToolbarFlexibleSpacer() : base(NSToolbar.NSToolbarFlexibleSpaceItemIdentifier) {
        }
    }
}
