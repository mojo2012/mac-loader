using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Drawing;

namespace MacLoader {
	public class SplitViewDelegate : NSSplitViewDelegate {
		public SplitViewDelegate() {
		}

		
		public override void Resize(NSSplitView splitView, System.Drawing.SizeF oldSize) {
			float width = splitView.Subviews[0].Bounds.Width;
			float height = splitView.Subviews[0].Bounds.Height;
			splitView.Subviews[0].SetBoundsSize(new SizeF(width, height));
		}
	}
}

