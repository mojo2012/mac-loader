using System;
using Eto.Forms;

namespace MacLoader.Actions {
    public class Close : ButtonAction {
        public const string ActionID = "close";
        
        public Close() {
            this.ID = ActionID;
            this.MenuText = "Close";
            this.ToolBarText = "Close";
            this.Accelerator = Application.Instance.CommonModifier | Key.W;
        }
        
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            
            Application.Instance.MainForm.Close();
        }
    }
}
