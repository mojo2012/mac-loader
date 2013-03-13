using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using System.Drawing;
using Eto.Forms;

namespace MacLoader.UI {
    public class PopOver : NSPopover {
        NSView contentView = null;
        bool closed = false;
        
        public bool Closed
        {
            get {
                return this.closed;
            }
            private set {
                closed = value;
            }
        }

        public PopOver() : this(new System.Drawing.RectangleF (0, 0, 300, 150)) {
        }

        public PopOver(NSView contentView) {
            Initialize(contentView);
        }

        public PopOver(Form container) : this((container.ControlObject as NSWindow).ContentView as NSView) {
        }

        public PopOver(Container container) : this(container.ControlObject as NSView) {
        }

        public PopOver(RectangleF bounds) {
            NSView contentView = new NSView(bounds);
            
            Initialize(contentView);
        }
        
        private void Initialize(NSView contentView) {
            this.ContentSize = new SizeF(contentView.Bounds.Width, contentView.Bounds.Height);
            this.Behavior = NSPopoverBehavior.Transient;
            this.ContentViewController = new Controller();

            this.contentView = contentView;
            
            this.ContentViewController = new NSViewController();
            this.ContentViewController.View = this.contentView;
            
            this.Delegate = new UIPopoverDelegate();
            
            this.Behavior = NSPopoverBehavior.Transient;
        }

        public void Show(NSView relativeToView) {
            Show(NSRectEdge.MinYEdge, relativeToView);
        }

        public void Show(NSRectEdge preferredEdge, NSView relativeToView) {
            base.Show(new RectangleF (0, 0, 0, 0), relativeToView, preferredEdge);
        }
    }
    
    class UIPopoverDelegate : NSPopoverDelegate {
        public override bool ShouldClose(NSPopover popover) {
            return true;
        }
    }

    public class Controller : NSViewController {
        public Controller(IntPtr handle) : base (handle) {
            Initialize();
        }
        
        [Export ("initWithCoder:")]
        public Controller(NSCoder coder) : base (coder) {
            Initialize();
        }
        
        public Controller(){
        }
        
        void Initialize() {
            
        }
    }
}

