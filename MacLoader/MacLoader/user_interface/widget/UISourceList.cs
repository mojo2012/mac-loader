using System;
using System.Drawing;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI.widget;
using MacLoader.UI.events;

namespace MacLoader.UI.widget {
	public class UISourceList : NSOutlineViewDelegate {
		#region fields
		NSOutlineView outlineView = null;
		NSView viewContainer = null;
		UISourceListDataSource dataSource = null;
		
		#endregion
	
		#region constructors
		public UISourceList(NSOutlineView outlineView) {
			this.viewContainer = outlineView.Superview;
			this.outlineView = outlineView;
			
			this.outlineView.Delegate = this;
			
			dataSource = new UISourceListDataSource();
			this.outlineView.DataSource = dataSource;
			
			this.outlineView.FloatsGroupRows = false;
			this.outlineView.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
		}
		#endregion
		
		#region public methods
		public void ExpandAllItems() {
			for (int x = 0; x<outlineView.RowCount; x++) {
				outlineView.ExpandItem(outlineView.ItemAtRow(x));
			}
		}
		#endregion

		#region public properties
		public List<UISourceListItem> Items {
			get {
				return dataSource.Items;
			}
			set {
				dataSource.Items = value;
				outlineView.ReloadData();
			}
		}
				
		public int SelectedIndex {
			get {
				return outlineView.SelectedRow;	
			}
		}
		
		public UISourceListItem SelectedItem {
			get {
				return (UISourceListItem)outlineView.ItemAtRow(SelectedIndex);	
			}
		}
		#endregion
		
		#region private methods
		private UISourceListItem CastItem(NSObject item) {
			return (UISourceListItem)item;
		}
		#endregion
		
		#region delegate methods
		public override bool ShouldEditTableColumn(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return false;
		}

		//not yet implemented in the NSOutlineViewDelegate class
		[Export ("outlineView:viewForTableColumn:item:")]
		public NSView GetViewForItem(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			NSView view = null;
			UISourceListItem sourceItem = CastItem(item);
			
			NSTextField textField = null;
			NSButton badge = null;
			
			if (tableColumn != null) {
				if (sourceItem.Icon != null) {
					view = outlineView.MakeView("ImageTextAndBadgeView", this);
					NSImageView iconView = (NSImageView)view.Subviews[0];
					iconView.Image = sourceItem.Icon;
					
					textField = (NSTextField)view.Subviews[1];
					
					badge = (NSButton)view.Subviews[2];
				} else {
					view = outlineView.MakeView("TextAndBadgeView", this);
					textField = (NSTextField)view.Subviews[0];
					badge = (NSButton)view.Subviews[1];
				}
				
				if (badge != null) {
					if (sourceItem.Badge == 0) {
						textField.Frame = new RectangleF(textField.Frame.Location, new SizeF(textField.Frame.Size.Width + 3 + badge.Frame.Width, textField.Frame.Size.Height));
						badge.Hidden = true;	
						badge.Title = "";
						
					} else {
						badge.Hidden = false;	
						badge.Title = sourceItem.Badge.ToString();
					}
				} 
					
				textField.StringValue = sourceItem.Name;
			} else {
				view = outlineView.MakeView("HeaderView", this);
					
				textField = (NSTextField)view.Subviews[0];
				textField.StringValue = sourceItem.Name.ToUpper();
			}
			
			return view;
		}
		
		[Export ("outlineView:dataCellForTableColumn:tableColumn:row")]
		public NSCell GetDataCell(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return base.GetCell(outlineView, tableColumn, item);
		}

		public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) {
			if ((CastItem(item)).IsHeader) {
				return false;
			} else {
				return true;
			}
		}

		public override bool IsGroupItem(NSOutlineView outlineView, NSObject item) {
			if ((CastItem(item)).IsHeader) {
				return true;
			} else {
				return false;
			}
		}

		public override float GetRowHeight(NSOutlineView outlineView, NSObject item) {
			if ((CastItem(item)).IsHeader) {
				return 23f;
			}
			
			return 20f;
		}

		public override void WillDisplayCell(NSOutlineView outlineView, NSObject cell, NSTableColumn tableColumn, NSObject item) {
			UISourceListItem sourceItem = CastItem(item);
			NSImageAndTextCell sourceCell = (NSImageAndTextCell)cell;
			
			sourceCell.Icon = sourceItem.Icon;
		}

		public override void SelectionDidChange(NSNotification notification) {
			if (SelectionChanged != null) {
				SelectionChanged(this, new UISourceListEventArgs(SelectedItem));
			}
		}

		public override bool ShouldCollapseItem(NSOutlineView outlineView, NSObject item) {
			return (CastItem(item)).Collapsable;
		}

		public override bool ShouldExpandItem(NSOutlineView outlineView, NSObject item) {
			return (CastItem(item)).Expandable;
		}
		#endregion
		
		
		#region events
		public event SelectionChangedEventHandler SelectionChanged;
		public delegate void SelectionChangedEventHandler(object sender,UISourceListEventArgs e);
		#endregion
	}
	
	class UISourceListDataSource : NSOutlineViewDataSource {
		List<UISourceListItem> rootItems = null;

		public List<UISourceListItem> Items {
			get {
				return this.rootItems;
			}
			set {
				rootItems = value;
			}
		}

		public UISourceListDataSource() : this(new List<UISourceListItem>()) {
		}
		
		public UISourceListDataSource(List<UISourceListItem> items) {
			rootItems = items;
		}
		
		public override int GetChildrenCount(NSOutlineView outlineView, NSObject item) {
			if (item == null) {
				return rootItems.Count;
			} else {
				return ((UISourceListItem)item).Children.Count;
			}
		}
		
		public override NSObject GetChild(NSOutlineView outlineView, int childIndex, NSObject ofItem) {
			if (ofItem != null) {
				return ((UISourceListItem)ofItem).Children[childIndex];
			} else {
				return rootItems[childIndex];
			}
		}

		public override NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			if (item != null) {
				UISourceListItem sourceItem = (UISourceListItem)item;
				
				string name = sourceItem.Name;
				
				if (sourceItem.IsHeader) {
					name = name.ToUpper();
				}
				
				return new NSString(name);
			} else {
				return new NSString("");
			}
		}

		public override bool ItemExpandable(NSOutlineView outlineView, NSObject item) {
			if (((UISourceListItem)item).Children.Count > 0) {
				return true;
			} else {
				return false;
			}
		}
	}
}

