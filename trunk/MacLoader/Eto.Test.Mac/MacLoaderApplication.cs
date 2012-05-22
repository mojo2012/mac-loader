using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using Eto.Forms;
using Eto.Platform.Mac.Forms.Controls;

namespace Eto.MacLoader {
	public class MacLoaderApplication : Application {
		public MacLoaderApplication(Generator generator)
			: base(generator) {
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

	public class Startup {
		public static void Main(string[] args) {
			AddStyles();
			
			var generator = new Eto.Platform.Mac.Generator();
			
			var app = new MacLoaderApplication(generator);
			app.Run(args);
			
		}

		static void AddStyles() {
			// support full screen mode!
			Style.Add<Window, NSWindow>("main", (widget, control) => {
				control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
			}
			);
			
			Style.Add<Application, NSApplication>("application", (widget, control) => {
				if (control.RespondsToSelector(new Selector("presentationOptions:"))) {
					control.PresentationOptions |= NSApplicationPresentationOptions.FullScreen;
				}
			}
			);

			// other styles
			Style.AddHandler<TreeGridViewHandler>("sectionList", (handler) => {
				handler.ScrollView.BorderType = NSBorderType.NoBorder;
				handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
			}
			);
		}
	}
}

