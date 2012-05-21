using System;
using System.Drawing;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI.widget;
using MacLoader.UI.events;

namespace MacLoader.UI.widget {

	//	public class UIOutlineViewColumnType : NS {
//		
//	}
	public class UIOutlineView : NSOutlineViewDelegate {
		#region fields
		NSOutlineView outlineView = null;
		UIOutlineViewDataSource dataSource = null;
		List<IUIOutlineViewColumn> columns = new List<IUIOutlineViewColumn>();
		
		#endregion
	
		#region constructors
		public UIOutlineView(NSOutlineView outlineView) {
			this.outlineView = outlineView;
			
			this.outlineView.Delegate = this;
			
			dataSource = new UIOutlineViewDataSource();
			this.outlineView.DataSource = dataSource;
			
			this.outlineView.FloatsGroupRows = true;
		}
		#endregion
		
		#region public methods
		public void ExpandAllRows() {
			for (int x = 0; x<outlineView.RowCount; x++) {
				outlineView.ExpandItem(outlineView.ItemAtRow(x));
			}
		}
		#endregion

		#region public properties
		public List<UIOutlineViewRow> Rows {
			get {
				return dataSource.Items;
			}
			set {
				dataSource.Items = value;
				outlineView.ReloadData();
			}
		}

		public List<IUIOutlineViewColumn> Columns {
			get {
				return this.columns;
			}
			set {
				columns = value;
			}
		}

		public int SelectedIndex {
			get {
				return outlineView.SelectedRow;	
			}
		}
		
		public UIOutlineViewRow SelectedRow {
			get {
				return (UIOutlineViewRow)outlineView.ItemAtRow(SelectedIndex);	
			}
		}
		#endregion
		
		#region private methods
		private UIOutlineViewRow CastItem(NSObject item) {
			return (UIOutlineViewRow)item;
		}
		#endregion
		
		#region delegate methods
		public override bool ShouldEditTableColumn(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return false;
		}

		//not yet implemented in the NSOutlineViewDelegate class
		[Export ("outlineView:viewForTableColumn:item:")]
		public NSView GetViewForItem(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			UIOutlineViewRow sourceItem = CastItem(item);
			
//			NSTextField textField = new NSTextField();
//			textField.StringValue = sourceItem.Name;
//			textField.Editable = false;
//			textField.Bordered = false;
//			
//			textField.DrawsBackground = false;
			
			NSView view = outlineView.MakeView(
				(string)tableColumn.Identifier.ToString(),
				this
			);
//			NSProgressIndicator view = new NSProgressIndicator();
			return view;
		}
		
		[Export ("outlineView:dataCellForTableColumn:tableColumn:row")]
		public NSCell GetDataCell(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			return base.GetCell(outlineView, tableColumn, item);
		}

		public override bool ShouldSelectItem(NSOutlineView outlineView, NSObject item) {
			return true;
		}

		public override bool IsGroupItem(NSOutlineView outlineView, NSObject item) {
			if ((CastItem(item)).IsHeader) {
				return true;
			} else {
				return false;
			}
		}

		public override float GetRowHeight(NSOutlineView outlineView, NSObject item) {
			return 18f;
		}

//		public override void WillDisplayCell(NSOutlineView outlineView, NSObject cell, NSTableColumn tableColumn, NSObject item) {
//			UIOutlineViewRow sourceItem = CastItem(item);
//			NSImageAndTextCell sourceCell = (NSImageAndTextCell)cell;
//			
//			sourceCell.Icon = sourceItem.Icon;
//		}

		public override void SelectionDidChange(NSNotification notification) {
			if (SelectionChanged != null) {
				SelectionChanged(this, new UIOutlineViewEventArgs(SelectedRow));
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
		public delegate void SelectionChangedEventHandler(object sender,UIOutlineViewEventArgs e);
		#endregion
	}
	
	class UIOutlineViewDataSource : NSOutlineViewDataSource {
		List<UIOutlineViewRow> rootItems = null;

		public List<UIOutlineViewRow> Items {
			get {
				return this.rootItems;
			}
			set {
				rootItems = value;
			}
		}

		public UIOutlineViewDataSource() : this(new List<UIOutlineViewRow>()) {
		}
		
		public UIOutlineViewDataSource(List<UIOutlineViewRow> items) {
			rootItems = items;
		}
		
		public override int GetChildrenCount(NSOutlineView outlineView, NSObject item) {
			if (item == null) {
				return rootItems.Count;
			} else {
				return ((UIOutlineViewRow)item).Children.Count;
			}
		}
		
		public override NSObject GetChild(NSOutlineView outlineView, int childIndex, NSObject ofItem) {
			if (ofItem != null) {
				return ((UIOutlineViewRow)ofItem).Children[childIndex];
			} else {
				return rootItems[childIndex];
			}
		}

		public override NSObject GetObjectValue(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item) {
			if (item != null) {
				UIOutlineViewRow sourceItem = (UIOutlineViewRow)item;
				
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
			if (((UIOutlineViewRow)item).Children.Count > 0) {
				return true;
			} else {
				return false;
			}
		}
	}
}

