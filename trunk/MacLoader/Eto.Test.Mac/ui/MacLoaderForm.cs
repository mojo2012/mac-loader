using System;
using Eto.Forms;
using Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MacLoader.Helpers;

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

			var icon = ResourceHelper.LoadIconFromBundle("toolbar_start.png");
			ButtonAction startDownloadsTB = new ButtonAction(
				"tb_start_downloads",
				"Start",
				icon,
				new EventHandler<EventArgs>(startDownloadsTB_Clicked)
			);

			startDownloadsTB.MenuText = "Start downloading";
			startDownloadsTB.ToolBarText = "Start downloading";
			startDownloadsTB.TooltipText = "Starts downloading all files.";

			// define action
			actions.Actions.Add(startDownloadsTB);



			// add action to toolbar
			actions.ToolBar.Add(startDownloadsTB.ID);
			
			// add action to file sub-menu
			var file = actions.Menu.FindAddSubMenu("&File");
			file.Actions.Add(startDownloadsTB.ID);
			
			// generate menu & toolbar
			this.Menu = actions.Menu.GenerateMenuBar();
			this.ToolBar = actions.ToolBar.GenerateToolBar();
		}



		#region event handling
		protected void startDownloadsTB_Clicked(object sender, EventArgs e) {
			MessageBox.Show(
					Application.Instance.MainForm,
					"You clicked me!",
					"Tutorial 2",
					MessageBoxButtons.OK
			);
		}

		#endregion
	}
}