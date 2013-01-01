using System;
using Eto.Forms;
using ED = Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MacLoader.Helpers;
using SD = System.Drawing;


namespace MacLoader.UI {
    public partial class MacLoaderForm : Form {
        #region event handling
        protected void tbStartDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbStartDownloads_Clicked");
        }

        protected void tbThrottleDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbThrottleDownloads_Clicked");
        }

        protected void tbAddURL_Clicked(object sender, EventArgs e) {
            Eto.Forms.Panel popoverView = new Panel();
            popoverView.Size = new ED.Size(450, 200);

            var layout = new DynamicLayout(popoverView);
            layout.BeginVertical();

            TextBox urlTextBox = new TextBox() { Text = "Enter URL here" };
            var addFileButton = new Button() { Text = "Add" };

            layout.Add(urlTextBox);
            layout.Add(addFileButton);

            layout.EndVertical();

            var popover = new PopOver(popoverView.ControlObject as NSView);
            popover.Show(sender as NSButton);
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

        public override void OnMaximized(EventArgs e) {
            base.OnMaximized(e);
        }
        
        public override void OnMinimized(EventArgs e) {
            base.OnMinimized(e);
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
