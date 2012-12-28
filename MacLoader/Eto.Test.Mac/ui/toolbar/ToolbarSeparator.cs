using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader {
    public class ToolbarSeparator : NSToolbarItem {
        public ToolbarSeparator() : base(NSToolbar.NSToolbarSeparatorItemIdentifier) {
        }
    }
}
