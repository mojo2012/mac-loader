using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI.widget;
using MacLoader.UI.events;

namespace MacLoader {
	public interface IUIOutlineViewColumn {
		String ColumnName {
			get;
			set;
		}
		
		float Width {
			get;
			set;
		}
		
		NSView CellTemplate {
			get;
		}
		
		int ColumnIndex {
			get;
		}
	}
	
	public abstract class UIOutlineViewColumn : NSTableColumn, IUIOutlineViewColumn {
		string columnName = "Column";
//		int width = 100;
		int columnIndex = -1;

		#region constructor
		public UIOutlineViewColumn(string columnName) {
			this.columnName = columnName;
		}
		#endregion

		#region IUIOutlineViewColumn implementation
		public string ColumnName {
			get {
				return columnName;
			}
			set {
				columnName = value;
			}
		}

//		public int Width {
//			get {
//				return width;
//			}
//			set {
//				width = value;
//			}
//		}
		
		public abstract NSView CellTemplate {
			get;
		}

		public int ColumnIndex {
			get {
				return columnIndex;
			}
		}
		#endregion
	}
	
	public class UIOutlineViewTextColumn : UIOutlineViewColumn {
		public override NSView CellTemplate {
			get {
				return new UIOutlineViewTextCell();
			}
		}
	}
	
	public class UIOutlineViewImageColumn : UIOutlineViewColumn {
		public override NSView CellTemplate {
			get {
				return new UIOutlineViewImageCell();
			}
		}
	}

	public class UIOutlineProgressBarColumn : UIOutlineViewColumn {
		public override NSView CellTemplate {
			get {
				return new UIOutlineViewProgressCell();
			}
		}
	}
}

