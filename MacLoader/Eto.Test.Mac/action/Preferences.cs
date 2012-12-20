using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.MacLoader.Actions {
    public class Preferences : ButtonAction {
        public const string ActionID = "preferences";
        
        public Preferences() {
            this.ID = ActionID;
            this.MenuText = "Preferences";
            this.ToolBarText = "Preferences";
        }
        
        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
//            var form = (MainForm)Application.Instance.MainForm;
//            
//            var dialog = new Dialogs.Preferences();
//            dialog.ShowDialog(Application.Instance.MainForm);
//            if (dialog.DialogResult == DialogResult.Ok) {
//                Notedown.Preferences.Folder = dialog.TextBoxFolder.Text;
//                Notedown.Preferences.Save();
//                form.Update();
//            }
        }
    }
}
