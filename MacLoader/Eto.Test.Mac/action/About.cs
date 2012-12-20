using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.MacLoader.Actions {
    public class About : ButtonAction {
        public const string ActionID = "about";
        
        public About() {
            this.ID = ActionID;
            this.MenuText = "About Notedown";
            this.ToolBarText = "About";
        }
        
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            
//            var about = new Dialogs.About();
//            about.ShowDialog(Application.Instance.MainForm);
        }
    }
}
