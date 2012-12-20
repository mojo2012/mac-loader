using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using Eto.Platform.Mac.Forms.Controls;
using Eto.Forms;

namespace Eto.MacLoader.Delegates {

    public class CustomTreeViewDelegate : TreeViewHandler.EtoOutlineDelegate {
        public bool AllowGroupSelection { get; set; }
        
        public override bool IsGroupItem(NSOutlineView outlineView, NSObject item) {
            return item != null && outlineView.LevelForItem(item) == 0;
        }
        
        public override void WillDisplayCell(NSOutlineView outlineView, NSObject cell, NSTableColumn tableColumn, NSObject item) {
            var textCell = cell as MacImageListItemCell;
            if (textCell != null) {
                textCell.UseTextShadow = true;

//                var parentItem = item as TreeViewHandler.EtoTreeItem;

                if (!this.IsGroupItem(outlineView, item)) {
                    textCell.SetGroupItem(false, outlineView, NSFont.SmallSystemFontSize, NSFont.SmallSystemFontSize);
                    textCell.BackgroundColor = NSColor.Blue;
                } else {
                    textCell.SetGroupItem(true, outlineView, NSFont.SystemFontSize, NSFont.SystemFontSize);
                }
            }
        }
        
        public override float GetRowHeight(NSOutlineView outlineView, NSObject item) {
            return 20;
        }
        
        public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) {
            return AllowGroupSelection || !IsGroupItem(outlineView, item);
        }
    }
}

