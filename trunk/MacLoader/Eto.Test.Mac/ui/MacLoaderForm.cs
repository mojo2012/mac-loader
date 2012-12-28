using System;
using Eto.Forms;
using Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MacLoader.Helpers;
using Eto.Platform.Mac.Forms.Controls;
using Eto.Platform.Mac;

namespace Eto.MacLoader {
    public partial class MacLoaderForm : Form {

        #region event handling
        protected void tbStartDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbStartDownloads_Clicked");
        }

        protected void tbThrottleDownloads_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbThrottleDownloads_Clicked");
        }

        protected void tbAddURL_Clicked(object sender, EventArgs e) {
            System.Console.WriteLine("tbAddURL_Clicked");

            var button = sender as NSButton;

            var popover = new PopOver();
                popover.Show(new System.Drawing.RectangleF (0, 0, 0, 0), button, NSRectEdge.MinYEdge);
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
