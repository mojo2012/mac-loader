using System;
using MonoMac.AppKit;
using System.Drawing;
using MonoMac.Foundation;

namespace MacLoader {
	public class UIPopover : NSPopover {
		NSView contentView = null;
		bool closed = false;
		
		public bool Closed {
			get {
				return this.closed;
			}
			private set {
				closed = value;
			}
		}

		public UIPopover(NSView contentView) {
			Initialize(contentView);
		}
		
		public UIPopover(RectangleF bounds) {
			NSView contentView = new NSView(bounds);
			
			Initialize(contentView);
		}
		
		private void Initialize(NSView contentView) {
			this.contentView = contentView;
		
			this.ContentViewController = new NSViewController();
			this.ContentViewController.View = this.contentView;
			
			this.Delegate = new UIPopoverDelegate();
			
			this.Behavior = NSPopoverBehavior.Transient;
		}

		public void Show(RectangleF relativePositioningRect, NSView positioningView, NSRectEdge preferredEdge, bool resetContentView) {
			if (resetContentView) {
				
			}
			
			base.Show(relativePositioningRect, positioningView, preferredEdge);
		}
	}
	
	class UIPopoverDelegate : NSPopoverDelegate {
		public override bool ShouldClose(NSPopover popover) {
			return true;
		}
	}
}

