using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Eto.Forms;
using Eto.Platform.Mac.Forms.Controls;
using Eto.Platform.Mac;
using Eto.MacLoader.Delegates;

namespace Eto.MacLoader {
    public class MacLoaderApplication : Application {
        public MacLoaderApplication(Generator generator) : base(generator) {
            this.Style = "application";
        }

        public override void OnInitialized(EventArgs e) {
            this.MainForm = new MacLoaderForm();
            HandleEvent(Application.TerminatingEvent);

            base.OnInitialized(e);

            // show the main form
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
            // support full screen mode!
            Style.Add<Window>("mainWindow", widget => {
                ((NSWindow)widget.ControlObject).CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
                ((NSWindow)widget.ControlObject).StyleMask |= NSWindowStyle.UnifiedTitleAndToolbar;
                ((NSWindow)widget.ControlObject).SetAutorecalculatesContentBorderThickness(false, NSRectEdge.MinYEdge);
                ((NSWindow)widget.ControlObject).SetContentBorderThickness(20f, NSRectEdge.MinYEdge);
            });

            Style.Add<ToolBarButton>("toolbarItemBezel", handler => {
                NSToolbarItem i = (NSToolbarItem)handler.ControlObject;
                NSButton b = (NSButton)i.View;
                 
                if (b == null) {
                    b = new NSButton(new System.Drawing.RectangleF(0f, 0f, 40f, 23f));
                    i.View = b;
                    b.Title = i.Label;
                    b.Image = (NSImage) handler.Icon.ControlObject;
                }

                b.BezelStyle = NSBezelStyle.TexturedRounded;
            });

            Style.Add<ToolBarHandler>("toolbar", handler => {
                handler.Control.AllowsUserCustomization = true;
                handler.Control.SizeMode = NSToolbarSizeMode.Small;
            });

            Style.Add<TreeViewHandler>("sidebar", h => {
                h.Control.Delegate = new CustomTreeViewDelegate { Handler = h, AllowGroupSelection = false };
                h.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
                h.Scroll.BorderType = NSBorderType.NoBorder;
                h.Control.FocusRingType = NSFocusRingType.None;
                NSOutlineView view = (NSOutlineView) h.Control;
            });

            Style.Add<TreeGridViewHandler>("downloadList", handler => {
                handler.ScrollView.BorderType = NSBorderType.NoBorder;
            });
        }
    }
}

