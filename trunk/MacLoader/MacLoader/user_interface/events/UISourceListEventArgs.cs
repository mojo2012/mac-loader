using System;
using MacLoader.UI.widget;

namespace MacLoader.UI.events {
	public class UISourceListEventArgs :EventArgs {
		UISourceListItem selectedItem = null;
			
		public UISourceListEventArgs(UISourceListItem item) {
			this.selectedItem = item;
		}

		public UISourceListItem SelectedItem {
			get {
				return this.selectedItem;
			}
		}
		
	}
}

