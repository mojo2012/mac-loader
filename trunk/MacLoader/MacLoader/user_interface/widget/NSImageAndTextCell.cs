using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.Drawing;

namespace MacLoader {
	public class NSImageAndTextCell : NSTextFieldCell {
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
		
		public NSImageAndTextCell() {
		}
		
		public NSImageAndTextCell(IntPtr handle) : base (handle) {
			
		}
		
		public NSImageAndTextCell(String imagePath, String text) {
			this.image = new NSImage(imagePath);
			this.StringValue = text;
		}

		public override void DrawWithFrame(RectangleF cellFrame, NSView inView) {
			float iconX = 0f;
			float iconY = 0f;
			float iconWidth = 0f;
			float iconHeight = 0f;
			
			RectangleF newRect = cellFrame;
			
			if (image != null) {
				iconX = cellFrame.X + 1;
				iconY = cellFrame.Y + 2;
				iconWidth = image.Size.Width;
				iconHeight = image.Size.Height;
				
				image.Draw(new RectangleF(iconX, iconY, iconWidth, iconHeight), new 
RectangleF(0, 0, iconWidth, iconHeight), NSCompositingOperation.SourceOver, 1.0f);
				
				newRect = new RectangleF(iconX + iconWidth, cellFrame.Y, cellFrame.Width - iconX - iconWidth, cellFrame.Height - 1);
			}
			
			newRect.Height = newRect.Height-1;
			
			base.DrawWithFrame(newRect, inView);
		}

		public override void DrawInteriorWithFrame(RectangleF cellFrame, NSView inView) {
			
			
			base.DrawInteriorWithFrame(cellFrame, inView);
		}
	}
}

