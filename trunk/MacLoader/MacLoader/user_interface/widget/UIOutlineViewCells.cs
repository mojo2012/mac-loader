using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MacLoader.UI.widget;
using MacLoader.UI.events;

namespace MacLoader {
	public class UIOutlineViewTextCell : NSTextField {
		public UIOutlineViewTextCell() {
			Editable = false;
			Bordered = false;
			
			DrawsBackground = false;
		}
		
		public string Value {
			get {
				return base.StringValue;
			}
			set {
				base.StringValue = value;
			}
		}
	}
	
	public class UIOutlineViewImageCell : NSImageView {
		public UIOutlineViewImageCell() {

		}
		
		public NSImage Value {
			get {
				return base.Image;
			}
			set {
				base.Image = value;
			}
		}
	}
	
	public class UIOutlineViewProgressCell : NSProgressIndicator {
		public UIOutlineViewProgressCell() {
			base.Style = NSProgressIndicatorStyle.Bar;
			base.Indeterminate = false;
			base.MinValue = 0f;
			base.MaxValue = 100f;
		}
		
		public double Value {
			get {
				return base.DoubleValue;
			}
			set {
				base.DoubleValue = value;
			}
		}
	}
	
}

