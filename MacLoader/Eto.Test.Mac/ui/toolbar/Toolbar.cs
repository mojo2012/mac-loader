using System;
using MonoMac.AppKit;
using System.Linq;
using System.Collections.Generic;

namespace Eto.MacLoader {
    public class Toolbar : NSToolbar {
        TBDelegate del = null;

        public Toolbar() : base("main") {
            SizeMode = NSToolbarSizeMode.Default;
            Visible = true;
            ShowsBaselineSeparator = true;
            AllowsUserCustomization = true;
            DisplayMode = NSToolbarDisplayMode.IconAndLabel;

            del = new TBDelegate();
            this.Delegate = del;
        }

        public List<NSToolbarItem> Items {
            get { return del.items; }
        }

        public void AddButton(String text, NSImage icon, String identifier) {
            del.AddItem(text, icon, identifier);

        }
    }

    class TBDelegate : NSToolbarDelegate {
        public List<NSToolbarItem> items = new List<NSToolbarItem>();
        
        public override string[] SelectableItemIdentifiers(NSToolbar toolbar) {
            return items.Where(r => r.Enabled).Select(r => r.Identifier).ToArray();
        }
        
        public override void WillAddItem(MonoMac.Foundation.NSNotification notification) {
            
        }
        
        public override void DidRemoveItem(MonoMac.Foundation.NSNotification notification) {
        }
        
        public override NSToolbarItem WillInsertItem(NSToolbar toolbar, string itemIdentifier, bool willBeInserted) {
            return items.FirstOrDefault(r => r.Identifier == itemIdentifier);
        }
        
        public override string[] DefaultItemIdentifiers(NSToolbar toolbar) {
            return items.Select(r => r.Identifier).ToArray();
        }
        
        public override string[] AllowedItemIdentifiers(NSToolbar toolbar) {
            return items.Where(r => r.Enabled).Select(r => r.Identifier).ToArray();
        }

        public void AddItem(String text, NSImage icon, String identifier) {
            NSButton b = new NSButton(new System.Drawing.RectangleF(0f, 0f, 40f, 23f)) { Title = text, BezelStyle = NSBezelStyle.TexturedRounded };
            var item = new NSToolbarItem(identifier) { Label = text, View = b };
            
            if (icon != null) {
                item.Image = icon;   
            }

            items.Add(item);
        }
    }
}