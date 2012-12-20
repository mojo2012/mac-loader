using System;
using Eto.Forms;

namespace Eto.MacLoader.Actions {
    public class Quit : ButtonAction {
        public const string ActionID = "quit";
        
        public Quit() {
            this.ID = ActionID;
            this.MenuText = "&Quit";
            this.ToolBarText = "Quit";
            this.Accelerator = Application.Instance.CommonModifier | Key.Q;
        }
        
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            
            Application.Instance.Quit();
        }
    }
}
