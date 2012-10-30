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

        }

        protected void tbThrottleDownloads_Clicked(object sender, EventArgs e) {

        }

        protected void mainForm_KeyDown(object sender, EventArgs e) {
            this.Close();
        }

        #endregion
    }
}
