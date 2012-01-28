using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Drawing;

namespace MacLoader.UI {
	public class NSImageAndTextView : NSTextField {
		NSImage image = null;

		public NSImage Icon {
			get {
				return this.image;
			}
			set {
				image = value;
			}
		}

		public String Text {
			get {
				return this.StringValue;
			}
			set {
				this.StringValue = value;
			}
		}
		
		private void Initialize() {
			//			base.LineBreakMode = NSLineBreakMode.TruncatingMiddle;
			base.DrawsBackground = false;
			base.RefusesFirstResponder = true;
			base.Editable = false;
			base.Bordered = false;
		}
		
		public NSImageAndTextView() {
			Initialize();
		}
		
		public NSImageAndTextView(IntPtr handle) : base (handle) {
			Initialize();
		}
		
		public NSImageAndTextView(NSImage image, String text) : this() {
			this.image = image;
			this.StringValue = text;
		}

		public override void DrawRect(RectangleF dirtyRect) {
			float iconX = 0f;
			float iconY = 0f;
			float iconWidth = 0f;
			float iconHeight = 0f;
			
			RectangleF newRect = dirtyRect;
			if (image != null) {
				iconX = dirtyRect.X;
				iconY = dirtyRect.Y + 2;
				iconWidth = image.Size.Width;
				iconHeight = image.Size.Height;
				
				image.Draw(new RectangleF(iconX, iconY, iconWidth, iconHeight), image.AlignmentRect, NSCompositingOperation.SourceOver, 1.0f, true, null);
				
				newRect = new RectangleF(iconX + iconWidth, dirtyRect.Y + 2, dirtyRect.Width - iconX - iconWidth, dirtyRect.Height);
			}
			
			base.DrawRect(newRect);
		}
	}
}

