using System;
using System.Drawing;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI;

namespace MacLoader.UI.widget {
	public	class UISourceListItem : NSObject {
		String name = "";
		List<UISourceListItem> children = new List<UISourceListItem>();
		NSImage icon = null;
		bool isHeader = false;
		int badge = 0;
		object data = null;

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

		public UISourceListItem(String name) {
			this.name = name;
		}
		
		public UISourceListItem(String name, NSImage icon) : this(name) {
			this.icon = icon;
		}

		public List<UISourceListItem> Children {
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

