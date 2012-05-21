using System;
using System.Drawing;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI;

namespace MacLoader.UI.widget {
//	public class UIOutlineViewCell : NSObject {
//		
//	}
	public	class UIOutlineViewRow : NSObject {
		String name = "";
		List<UIOutlineViewRow> children = new List<UIOutlineViewRow>();
		NSImage icon = null;
		bool isHeader = false;
		int badge = 0;
		bool isCollapsable = true;
		bool isExpandable = true;
		object data = null;

		#region constructors
		public UIOutlineViewRow() {

		}
		
//		public UIOutlineViewRow(String name, NSImage icon) : this() {
//			this.icon = icon;
//		}
		#endregion


		public bool Collapsable {
			get {
				return this.isCollapsable;
			}
			set {
				isCollapsable = value;
			}
		}

		public bool Expandable {
			get {
				return this.isExpandable;
			}
			set {
				isExpandable = value;
			}
		}

		public object DataObject {
			get {
				return this.data;
			}
			set {
				data = value;
			}
		}

		public int Badge {
			get {
				return this.badge;
			}
			set {
				badge = value;
			}
		}

		public bool IsHeader {
			get {
				return this.isHeader;
			}
			set {
				isHeader = value;
			}
		}

		public List<UIOutlineViewRow> Children {
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
}

