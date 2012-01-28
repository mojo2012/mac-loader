using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Drawing;

namespace MacLoader {
	public class SplitViewDelegate : NSSplitViewDelegate {
		float minSidebarWidth = 120;
		float maxSidebarWidth = 300;
		
		public SplitViewDelegate() {
		}

		
//		public override void Resize(NSSplitView splitView, SizeF oldSize) {
//			float width = splitView.Subviews[0].Bounds.Width;
//			float height = splitView.Subviews[0].Bounds.Height;
//			splitView.Subviews[0].SetBoundsSize(new SizeF(width, height));
//		}

		public override bool ShouldAdjustSize(NSSplitView splitView, NSView view) {
			if (splitView.Subviews[0] == view) {
				return false;
			}
			
			return true;
		}
		
//		[Export ("splitView:resizeSubviewsWithOldSize:")]
//		public void ResizeSubviewsWithOldSize(NSSplitView splitView, SizeF oldSize) {
//			return;
//		}

		public override float ConstrainSplitPosition(NSSplitView splitView, float proposedPosition, int subviewDividerIndex) {
			if (proposedPosition < minSidebarWidth) {
				return minSidebarWidth;
			} else if (proposedPosition > maxSidebarWidth) {
				return maxSidebarWidth;
			}
			
			return proposedPosition;
		}
	}
}

