using System;
using Eto.Forms;

namespace Eto.MacLoader {
    public class SearchAction : ButtonAction {
        
        public SearchAction(string id, string text, EventHandler<EventArgs> activated)
            : base(id, text, activated) {
        }
        
        public SearchAction(string id, string text)
            : base(id, text) {
        }
        
        public SearchAction() {
        }
        
        public override ToolBarItem Generate(ActionItem actionItem, ToolBar toolBar) {
            ToolBarButton tbb = new ToolBarButton(toolBar.Generator);
            tbb.ID = this.ID;
            tbb.Enabled = this.Enabled;
            if (ShowLabel || actionItem.ShowLabel || toolBar.TextAlign != ToolBarTextAlign.Right)
                tbb.Text = ToolBarText;
            //Console.WriteLine("Adding toolbar {0}", ToolBarText);
            if (Icon != null)
                tbb.Icon = Icon;
            
            if (!string.IsNullOrEmpty(ToolBarItemStyle))
                tbb.Style = ToolBarItemStyle;
            new ToolBarConnector(this, tbb);
            return tbb;
        }
        
        protected new class ToolBarConnector {
            ToolBarButton toolBarButton;
            SearchAction action;

            public ToolBarConnector(SearchAction action, ToolBarButton toolBarButton) {
                this.toolBarButton = toolBarButton;
                this.toolBarButton.Click += toolBarButton_Click;
                this.action = action;
                this.action.EnabledChanged += new EventHandler<EventArgs>(action_EnabledChanged).MakeWeak(e => this.action.EnabledChanged -= e);
            }
            
            void toolBarButton_Click(Object sender, EventArgs e) {
                action.OnActivated(e);
            }
            
            void action_EnabledChanged(Object sender, EventArgs e) {
                toolBarButton.Enabled = action.Enabled;
            }
        }
    }
}

