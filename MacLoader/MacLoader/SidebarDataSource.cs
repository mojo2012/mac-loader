using System;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace MacLoader {
	public class SidebarDataSource : NSOutlineViewDataSource {
		List<SidebarItem> rootItems = null;
		
		public SidebarDataSource(List<SidebarItem> items) {
			rootItems = items;
		}
		
		public override int GetChildrenCount(NSOutlineView outlineView, NSObject item) {
			if (item == null) {
				return rootItems.Count;
			} else {
				return ((SidebarItem)item).Children.Count;
			}
		}
		
		public override NSObject GetChild(NSOutlineView outlineView, int childIndex, NSObject ofItem) {
			if (ofItem != null) {
				return ((SidebarItem)ofItem).Children[childIndex];
			} else {
				return rootItems[childIndex];
			}
		}

		public override NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			if (item != null) {
				SidebarItem sidebarItem = (SidebarItem)item;
				
				string name = sidebarItem.Name;
				
				if (sidebarItem.IsHeader) {
					name = name.ToUpper();
					//outlineView.ExpandItem(item,true);
				}
				
				return new NSString(name);
			} else {
				return new NSString("");
			}
		}

		public override bool ItemExpandable(NSOutlineView outlineView, NSObject item) {
			if (((SidebarItem)item).Children.Count > 0) {
				return true;
			} else {
				return false;
			}
		}
	}
	
	public class SidebarItem : NSObject {
		String name = "";
		List<SidebarItem> children = new List<SidebarItem>();
		NSImage icon = null;
		bool isHeader = false;

		public bool IsHeader {
			get {
				return this.isHeader;
			}
			set {
				isHeader = value;
			}
		}

		public SidebarItem(String name) {
			this.name = name;
		}
		
		public SidebarItem(String name, NSImage icon) : this(name) {
			this.icon = icon;
		}

		public List<SidebarItem> Children {
			get {
				return this.children;
			}
			set {
				children = value;
			}
		}

		public String Name {
			get {
				return this.name;
			}
			set {
				name = value;
			}
		}

		public NSImage Icon {
			get {
				return this.icon;
			}
			set {
				icon = value;
			}
		}
	}
	
	public class SidebarDelegate : NSOutlineViewDelegate {
		public override bool ShouldEditTableColumn(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return false;
		}

		public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) {
			if (((SidebarItem)item).IsHeader) {
				return false;
			} else {
				return true;
			}
		}

		public override bool IsGroupItem(NSOutlineView outlineView, NSObject item) {
			if (((SidebarItem)item).IsHeader) {
				return true;
			} else {
				return false;
			}
		}

		public override float GetRowHeight(NSOutlineView outlineView, NSObject item) {
			return 21f;
		}

		public override void WillDisplayCell(NSOutlineView outlineView, NSObject cell, NSTableColumn tableColumn, NSObject item) {
			SidebarItem sidebarItem = (SidebarItem)item;
			NSImageAndTextCell sidebarCell = (NSImageAndTextCell)cell;
			
			sidebarCell.Icon = sidebarItem.Icon;
		}
	}
}

