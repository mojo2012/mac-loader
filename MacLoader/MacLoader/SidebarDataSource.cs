using System;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.Foundation;
using System.Drawing;

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
	
	public class SidebarDelegate : NSOutlineViewDelegate { //NSOutlineViewDelegate {
		public override bool ShouldEditTableColumn(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return false;
		}

		//not yet implemented in the NSOutlineViewDelegate class
		[Export ("outlineView:viewForTableColumn:item:")]
		public NSView GetViewForItem(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			NSView view = null;
			SidebarItem sidebarItem = (SidebarItem)item;
			
			NSTextField textField = null;
			
			if (tableColumn != null) {
				if (sidebarItem.Icon != null) {
					view = outlineView.MakeView("ImageAndTextCell", this);
					NSImageView iconView = (NSImageView)view.Subviews[0];
					iconView.Image = sidebarItem.Icon;
					
					textField = (NSTextField)view.Subviews[1];
				} else {
					view = outlineView.MakeView("TextCell", this);
					textField = (NSTextField)view.Subviews[0];
				}
					
				textField.StringValue = sidebarItem.Name;
			} else {
				view = outlineView.MakeView("HeaderCell", this);
					
				textField = (NSTextField)view.Subviews[0];
				textField.StringValue = sidebarItem.Name.ToUpper();
			}
			
			
			
			return view;
		}
		
//		[Export ("outlineView:dataCellForTableColumn:tableColumn:row")]
//		public NSCell GetDataCell(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
//			return base.GetCell(outlineView, tableColumn, item);
//		}

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
			if (((SidebarItem)item).IsHeader) {
				return 23f;
			}
			
			return 20f;
		}

		public override void WillDisplayCell(NSOutlineView outlineView, NSObject cell, NSTableColumn tableColumn, NSObject item) {
			SidebarItem sidebarItem = (SidebarItem)item;
			NSImageAndTextCell sidebarCell = (NSImageAndTextCell)cell;
			
			sidebarCell.Icon = sidebarItem.Icon;
		}
	}
}

