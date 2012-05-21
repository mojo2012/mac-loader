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
//		List<IUIOutlineViewColumn> columns = new List<IUIOutlineViewColumn>();

		bool uninitialized = true;

		enum UIOutlineViewColumnType {
			TextColumn,
			ImageColumn,
			ProgressBarColumn,
		}

		#endregion
	
		#region constructors
		public UIOutlineView(NSOutlineView outlineView) {
			this.outlineView = outlineView;
			
			this.outlineView.Delegate = this;
			
			dataSource = new UIOutlineViewDataSource();
			this.outlineView.DataSource = dataSource;
			
			this.outlineView.FloatsGroupRows = true;

			//delete all columsn defined in Interface Builder
			foreach (var column in outlineView.TableColumns()) {
				System.Console.WriteLine("column deleted:");

				this.outlineView.RemoveColumn(column);
			}
		}
		#endregion
		
		#region public methods
		public void ExpandAllRows() {
			for (int x = 0; x<outlineView.RowCount; x++) {
				outlineView.ExpandItem(outlineView.ItemAtRow(x));
			}
		}

		public void Reload() {
			outlineView.ReloadData();
		}
		#endregion

		#region public properties
		public List<UIOutlineViewRow> Rows {
			get {
				return dataSource.Items;
			}
//			set {
//				dataSource.Items = value;
//
//				outlineView.ReloadData();
//			}
		}

		public void AddColumn(UIOutlineViewColumn column) {
			column.ColumnIndex = this.outlineView.ColumnCount;

//			this.columns.Add(column);
			this.outlineView.AddColumn(column);


			if (uninitialized & this.outlineView.ColumnCount == 2) {
				uninitialized = false;
				this.outlineView.RemoveColumn(this.outlineView.TableColumns()[0]);
			}
		}

		public void RemoveColumn(UIOutlineViewColumn column) {
//			this.columns.Remove(column);
			this.outlineView.RemoveColumn(column);
		}

		public List<UIOutlineViewColumn> Columns {
			get {
				if (!uninitialized) {
					var castTmpCols = new List<UIOutlineViewColumn>();

					try {
						foreach (var col in this.outlineView.TableColumns()) {
							castTmpCols.Add((UIOutlineViewColumn)col);
						}
					} catch (Exception ex) {
						System.Console.WriteLine(ex.Message);
					}

				}

				return new List<UIOutlineViewColumn>();
			}
//			set {
//				columns = value;
//			}
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

			NSView view = outlineView.MakeView(
				(string)tableColumn.Identifier.ToString(),
				this
			);

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

