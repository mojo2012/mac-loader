using System;
using Eto.Forms;
using ED = Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MacLoader.Helpers;
using SD = System.Drawing;

namespace MacLoader.UI {
    public partial class MacLoaderForm  {
        #region event handling
        protected void tbStartDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbStartDownloads_Clicked");
        }

        protected void tbThrottleDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbThrottleDownloads_Clicked");
        }

        protected void tbAddURL_Clicked(object sender, EventArgs e) {
            var testWindow = new Dialog() { Size = new ED.Size(450, 200) };
            testWindow.DisplayMode = DialogDisplayMode.Attached;

//            var popoverView = new Panel() { Size = new ED.Size(450, 200) };
				//var layout = new DynamicLayout(testWindow);
				DynamicLayout layout = null;
				layout.Padding = new ED.Padding(10, 10, 10, 10);

            layout.BeginVertical();

            var urlTextBoxLabel = new Label() { Text = "Enter links here:", TextColor = ED.Colors.Black  };
            var urlTextBox = new TextArea() { Size = new ED.Size(200, 100) };
            urlTextBox.KeyDown += (se, ev) => { 
					 //if (ev.Key == Key.Escape)
					 //    testWindow.Close();
            };
            var addFileButton = new Button() { Text = "Analyze" };

//            testWindow.AddDockedControl(addFileButton, null);
//            testWindow.AddDockedControl(urlTextBox, null);

            layout.Add(urlTextBoxLabel, true, false);
            layout.Add(urlTextBox, true, false);

            layout.BeginHorizontal();

            layout.Add(null, true);
            layout.Add(addFileButton, false, false);
            layout.EndHorizontal();

            layout.EndVertical();

            testWindow.ShowDialog(this);
        }

        protected void tbFilterDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbFilterDownloads_Clicked");
        }

        protected void tbFilterDownloads_Changed(object sender, EventArgs e) {
            System.Console.WriteLine("tbFilterDownloads_Changed");
        }

        protected void mainForm_KeyDown(object sender, EventArgs e) {
            this.Close();
        }

        public override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            base.OnClosing(e);
            /*
             * Note that on OS X, windows usually close, but the application will keep running.  It is
             * usually better to handle the Application.OnTerminating event instead.
             * 
            var result = MessageBox.Show (this, "Are you sure you want to close?", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) e.Cancel = true;
            */
        }

        public override void OnClosed(EventArgs e) {
            base.OnClosed(e);
        }

        #endregion
    }
}
