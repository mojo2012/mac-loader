using System;
using Eto.Forms;
using Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace Eto.MacLoader {
	public class MacLoaderForm : Form {
		public MacLoaderForm() {
			SetupUserInterface();
		}

		public MacLoaderForm(Generator generator):base(generator) {
			SetupUserInterface();
		}

		public void SetupUserInterface() {
			this.ClientSize = new Size(600, 400);
			this.Title = "MacLoader";

			GenerateActions();
		}

		void GenerateActions() {
			var actions = new GenerateActionArgs(this);
				
			// define action
			actions.Actions.Add(new MyAction());

			// add action to toolbar
			actions.ToolBar.Add(MyAction.ActionID);
			
			// add action to file sub-menu
			var file = actions.Menu.FindAddSubMenu("&File");
			file.Actions.Add(MyAction.ActionID);
			
			// generate menu & toolbar

			this.Menu = actions.Menu.GenerateMenuBar();
			
			this.ToolBar = actions.ToolBar.GenerateToolBar();
		}

		public class MyAction : ButtonAction {
			public const string ActionID = "my_action";
		
			public MyAction() {
				this.ID = ActionID;
				this.MenuText = "C&lick Me";
				this.ToolBarText = "Click Me";
				this.TooltipText = "This shows a dialog for no reason";
				this.Accelerator = Application.Instance.CommonModifier | Key.M;  // control+M or cmd+M
			}
		
			protected override void OnActivated(EventArgs e) {
				base.OnActivated(e);
			
				MessageBox.Show(
					Application.Instance.MainForm,
					"You clicked me!",
					"Tutorial 2",
					MessageBoxButtons.OK
				);
			}
		}

		static void AddStyles() {
			// support full screen mode!
			Eto.Style.Add<Window, NSWindow>("main", (widget, control) => {
				control.CollectionBehavior |= NSWindowCollectionBehavior.FullScreenPrimary;
			}
			);
			
			Eto.Style.Add<Application, NSApplication>("application", (widget, control) => {
				if (control.RespondsToSelector(new Selector("presentationOptions:"))) {
					control.PresentationOptions |= NSApplicationPresentationOptions.FullScreen;
				}
			}
			);

			// other styles
//			Eto.Style.AddHandler<TreeGridViewHandler>("sectionList", (handler) => {
//				handler.ScrollView.BorderType = NSBorderType.NoBorder;
//				handler.Control.SelectionHighlightStyle = NSTableViewSelectionHighlightStyle.SourceList;
//			}
//			);
		}
	}
}