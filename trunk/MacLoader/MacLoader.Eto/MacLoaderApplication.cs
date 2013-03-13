using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Eto.Forms;
using Eto.Platform.Mac.Forms.Controls;
using Eto.Platform.Mac;
using Eto.MacLoader.Delegates;
using MacLoader.UI;

namespace Eto.MacLoader {
    public class MacLoaderApplication : Application {
        public MacLoaderApplication(Generator generator) : base(generator) {
            this.Style = "application";
        }

        public override void OnInitialized(EventArgs e) {
            this.MainForm = new MacLoaderForm();
            HandleEvent(Application.TerminatingEvent);

            base.OnInitialized(e);
            this.MainForm.Show();
        }

        public override void OnTerminating(System.ComponentModel.CancelEventArgs e) {
            base.OnTerminating(e);

            var result = MessageBox.Show(
                    this.MainForm,
                    "Are you sure you want to quit?",
                    MessageBoxButtons.YesNo,
                    MessageBoxType.Question
            );

            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }


    //Entry point for application startup
    public class Startup {
        public static void Main(string[] args) {
            var generator = new Eto.Platform.Mac.Generator();

            var app = new MacLoaderApplication(generator);
                
            AddStyles();

            app.Run(args);
        }

        static void AddStyles() {
            Style.Add<Window>("mainWindow", widget => {
                var window = widget.ControlObject as NSWindow;

                // support full screen mode!
                window.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
                window.StyleMask |= NSWindowStyle.UnifiedTitleAndToolbar;
                window.SetAutorecalculatesContentBorderThickness(false, NSRectEdge.MinYEdge);
                window.SetContentBorderThickness(20f, NSRectEdge.MinYEdge);
                window.StyleMask |= NSWindowStyle.TexturedBackground;

//                var superView = window.ContentView.Superview;
//
//                var r = superView.Frame;
//                r.Height = 100;
//
//                superView.Frame = r;
//                superView.NeedsDisplay = true;

//                var titlebar = superView.Subviews;
            });

//            Style.Add<ToolBarButton>("toolbarItemBezel", handler => {
//                NSToolbarItem i = (NSToolbarItem)handler.ControlObject;
//                NSButton b = (NSButton)i.View;
//                 
//                if (b == null) {
//                    b = new NSButton(new System.Drawing.RectangleF(0f, 0f, 40f, 23f));
//                    i.View = b;
//                    b.Title = i.Label;
//                    b.Image = (NSImage) handler.Icon.ControlObject;
//                }
//
//                b.BezelStyle = NSBezelStyle.TexturedRounded;
//            });

//            Style.Add<ToolBarHandler>("toolbar", handler => {
//                handler.Control.AllowsUserCustomization = true;
//                handler.Control.SizeMode = NSToolbarSizeMode.Small;
//            });

            Style.Add<TreeViewHandler>("sidebar", h => {
                var view = h.Control as NSOutlineView;

                view.Delegate = new TreeViewDelegate { Handler = h, AllowGroupSelection = false };
                view.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
                view.FocusRingType = NSFocusRingType.None;
                view.FloatsGroupRows = true;

                h.Scroll.BorderType = NSBorderType.NoBorder;
            });

            Style.Add<TreeGridViewHandler>("downloadList", handler => {
                handler.ScrollView.BorderType = NSBorderType.NoBorder;
            });
        }
    }
}

