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
        public MacLoaderForm() {
            SetupUserInterface();
        }

        public MacLoaderForm(Generator generator):base(generator) {
            SetupUserInterface();
        }

        public void SetupUserInterface() {
            this.ClientSize = new Size(600, 400);
            this.Title = "MacLoader";
            this.Style = "mainWindow";

            //menus and toolbar
            GenerateActions();

            //main form
            GenerateWindowContent();

            //events

        }

        void GenerateWindowContent() {
            Splitter contentSplittler = new Splitter();
            this.AddDockedControl(contentSplittler);

            contentSplittler.FixedPanel = SplitterFixedPanel.Panel1;
            contentSplittler.Position = 160;

            Eto.Forms.TreeGridView sidebar = new Eto.Forms.TreeGridView();
            sidebar.Style = "sidebar";
//            list..Add(new ListItem { Text="TEST" });

//            sidebar.DataStore = new Grid
//
//            contentSplittler.Panel1 = sidebar;
        }

        void GenerateActions() {
            // use actions to generate menu & toolbar to share logic
            var actions = new GenerateActionArgs(this);

            // generate actions to use in menus and toolbars
            Application.Instance.GetSystemActions(actions);

            GenerateMenus(actions);
            GenerateToolbar(actions);

            System.Console.WriteLine("break");
        }

        void GenerateMenus(GenerateActionArgs args) {
            var main = args.Menu.FindAddSubMenu("MacLoader", 1);
            var file = args.Menu.FindAddSubMenu("&File", 100);
            var edit = args.Menu.FindAddSubMenu("&Edit", 200);
            var window = args.Menu.FindAddSubMenu("&Window", 900);
            var help = args.Menu.FindAddSubMenu("Help", 1000);

            if (Generator.ID == "mac") {
                // have a nice OS X style menu

//              main.Actions.Add("1", 0);
//              main.Actions.AddSeparator();
//              main.Actions.Add("mac_hide", 700);
//              main.Actions.Add("mac_hideothers", 700);
//              main.Actions.Add("mac_showall", 700);
//              main.Actions.AddSeparator(900);
//              main.Actions.Add("2", 1000);

                main.Actions.Add("mac_performClose", 900);


                edit.Actions.Add("mac_undo", 100);
                edit.Actions.Add("mac_redo", 100);
                edit.Actions.AddSeparator(200);
                edit.Actions.Add("mac_cut", 200);
                edit.Actions.Add("mac_copy", 200);
                edit.Actions.Add("mac_paste", 200);
                edit.Actions.Add("mac_delete", 200);
                edit.Actions.Add("mac_selectAll", 200);

                window.Actions.Add("mac_performMiniaturize");
                window.Actions.Add("mac_performZoom");
            }

            this.Menu = args.Menu.GenerateMenuBar();
        }

        void GenerateToolbar(GenerateActionArgs args) {

            //start downlading button
            var icon = ResourceHelper.LoadIconFromBundle("toolbar_start.png");
            ButtonAction startDownloadsTB = new ButtonAction(
                    "tb_start_downloads",
                    "Start",
                    icon,
                    new EventHandler<EventArgs>(tbStartDownloads_Clicked)
            );

            startDownloadsTB.ToolBarText = "Start downloads";
            startDownloadsTB.TooltipText = "Starts downloading all files.";
            startDownloadsTB.ToolBarItemStyle = "toolbarItemBezel";

            //limit download speed button
            icon = ResourceHelper.LoadIconFromBundle("toolbar_speed_limit.png");
            ButtonAction limitDownloadSpeedDownloadsTB = new ButtonAction(
                "tb_limit_download_speed",
                "Limit download speed",
                icon,
                new EventHandler<EventArgs>(tbThrottleDownloads_Clicked)
            );
            
            limitDownloadSpeedDownloadsTB.ToolBarText = "Limit speed";
            limitDownloadSpeedDownloadsTB.TooltipText = "Limit download speed";
            limitDownloadSpeedDownloadsTB.ToolBarItemStyle = "toolbarItemBezel";

            //add link
            icon = ResourceHelper.LoadIconFromBundle("toolbar_add.png");
            ButtonAction addURLTB = new ButtonAction(
                "tb_add_url",
                "Add URL",
                icon,
                new EventHandler<EventArgs>(tbAddURL_Clicked)
            );
            
            addURLTB.ToolBarText = "Add URL";
            addURLTB.TooltipText = "Add URL to download";
            addURLTB.ToolBarItemStyle = "toolbarItemBezel";

            // add action
            addAction(args, startDownloadsTB);
            addAction(args, limitDownloadSpeedDownloadsTB);
            addAction(args, addURLTB);

            this.ToolBar = args.ToolBar.GenerateToolBar();
            this.ToolBar.Style = "toolbar";
        }

        private void addAction(GenerateActionArgs args, Eto.Forms.BaseAction action) {
            args.Actions.Add(action);
            args.ToolBar.Add(action.ID);
        }
    }
}
