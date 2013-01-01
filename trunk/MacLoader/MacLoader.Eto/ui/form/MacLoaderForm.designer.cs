using System;
using Eto.Forms;
using Eto.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using MacLoader.Helpers;
using System.Collections.Generic;
using System.Collections;
using MacLoader.UI;
using Eto;

namespace MacLoader.UI {
    public partial class MacLoaderForm : Form {
        TreeView sidebar = null;

        public MacLoaderForm() {
            SetupUserInterface();

            this.LoadComplete += OnLoadComplete;
        }

        public MacLoaderForm(Generator generator):base(generator) {
            SetupUserInterface();
        }

        public void SetupUserInterface() {
            this.ClientSize = new Size(800, 500);
            this.Title = "MacLoader";
            this.Style = "mainWindow";

            //menus and toolbar
            GenerateActions();

            //main form
            GenerateWindowContent();

            //events
            HandleEvent(MacLoaderForm.MaximizedEvent, MacLoaderForm.MinimizedEvent);
            HandleEvent(MacLoaderForm.ClosedEvent, MacLoaderForm.ClosingEvent);
        }

        void GenerateWindowContent() {
            createToolbar();

            DynamicLayout layout = new DynamicLayout(this);
            layout.DefaultPadding = new Padding() {Left = 0, Right = 0, Bottom = 10, Top = 0};
            layout.BeginVertical();

            //splitter
            Splitter contentSplittler = new Splitter();
            contentSplittler.FixedPanel = SplitterFixedPanel.Panel1;
            contentSplittler.Position = 150;

            //sidebar
            sidebar = new Eto.Forms.TreeView();
            sidebar.Style = "sidebar";
            sidebar.DataStore = new SidebarStore();
            contentSplittler.Panel1 = sidebar;

            //download list
            TreeGridView downloadList = new TreeGridView();
            downloadList.Style = "downloadList";

            downloadList.Columns.Add(new GridColumn { HeaderText = "File", Width = 230, AutoSize = false, Sortable = true });
            downloadList.Columns.Add(new GridColumn() { HeaderText = "Hoster", Width = 90, AutoSize = false, Sortable = true  });
            downloadList.Columns.Add(new GridColumn() { HeaderText = "Status", Width = 120, AutoSize = false, Sortable = true  });
            downloadList.Columns.Add(new GridColumn() { HeaderText = "%", Width = 120, AutoSize = false, Sortable = true  });
            
            contentSplittler.Panel2 = downloadList;

            layout.Add(contentSplittler, true, true);
            //this.AddDockedControl(contentSplittler);

            layout.EndVertical();
        }

        private void createToolbar() {
            var toolbar = new Toolbar();

            var icon = ResourceHelper.LoadNSImageFromBundle("toolbar_start.png");
            var button = new ToolbarButton("Start downloads", icon);
            button.Clicked += tbStartDownloads_Clicked;
            toolbar.Items.Add(button);

            icon = ResourceHelper.LoadNSImageFromBundle("toolbar_speed_limit.png");
            button = new ToolbarButton("Limit speed", icon);
            button.Clicked += tbThrottleDownloads_Clicked;
            toolbar.Items.Add(button);

            icon = ResourceHelper.LoadNSImageFromBundle("toolbar_add.png");
            button = new ToolbarButton("Add URL", icon);
            button.Clicked += tbAddURL_Clicked;
            toolbar.Items.Add(button);

            toolbar.Items.Add(new ToolbarFlexibleSpacer());

            var filterBox = new ToolbarSearchField("Filter download list");
            filterBox.Changed += tbFilterDownloads_Changed;
            toolbar.Items.Add(filterBox);

            var window = this.ControlObject as NSWindow;
            window.Toolbar = toolbar;
        }

        private class SidebarStore : TreeItemCollection {
            public SidebarStore() {
                var downloads = new TreeItem { Text = "Downloads", Key = "Downloads" };
                var linkGrabber = new TreeItem { Text = "Link Grabber", Key = "Link Grabber" };
                var labels = new TreeItem { Text = "Labels", Key = "Labels" };

                downloads.Children.Add(new TreeItem { Text = "All", Image = ResourceHelper.LoadIconFromBundle("status-downloading.png") });
                downloads.Children.Add(new TreeItem { Text = "Downloading", Image = ResourceHelper.LoadIconFromBundle("status-downloading.png") });
                downloads.Children.Add(new TreeItem { Text = "Completed", Image = ResourceHelper.LoadIconFromBundle("status-completed.png") });
                downloads.Children.Add(new TreeItem { Text = "Paused", Image = ResourceHelper.LoadIconFromBundle("status-paused.png") });

                linkGrabber.Children.Add(new TreeItem { Text = "All", Image = ResourceHelper.LoadIconFromBundle("status-not-available.png") });
                linkGrabber.Children.Add(new TreeItem { Text = "Processing", Image = ResourceHelper.LoadIconFromBundle("status-processing.png") });
                linkGrabber.Children.Add(new TreeItem { Text = "Available", Image = ResourceHelper.LoadIconFromBundle("status-online.png") });
                linkGrabber.Children.Add(new TreeItem { Text = "Offline", Image = ResourceHelper.LoadIconFromBundle("status-offline.png") });

                labels.Children.Add(new TreeItem { Text = "No Label" });

                this.Add(downloads);
                this.Add(linkGrabber);
                this.Add(labels);
            }
        }

        public class SidebarEntry : TreeGridItem {

        }

        void GenerateActions() {
            // use actions to generate menu & toolbar to share logic
            var actions = new GenerateActionArgs(this);

            // generate actions to use in menus and toolbars
            Application.Instance.GetSystemActions(actions);

            actions.Actions.Add(new Actions.About());
            actions.Actions.Add(new Actions.Quit());
            actions.Actions.Add(new Actions.Close());
            actions.Actions.Add(new Actions.Preferences());

            GenerateMenus(actions);
//            GenerateToolbar(actions);
        }

        void GenerateMenus(GenerateActionArgs args) {
            var main = args.Menu.FindAddSubMenu("MacLoader", 0);
            var file = args.Menu.FindAddSubMenu("&File", 0);
            var edit = args.Menu.FindAddSubMenu("&Edit", 0);
            var window = args.Menu.FindAddSubMenu("&Window", 1000);
//            var help = args.Menu.FindAddSubMenu("Help", 1000);
            
            main.Actions.Add(Actions.About.ActionID);
            main.Actions.AddSeparator();
            main.Actions.Add(Actions.Preferences.ActionID);
            main.Actions.AddSeparator();
            main.Actions.Add("mac_hide");
            main.Actions.Add("mac_hideothers");
            main.Actions.Add("mac_showall");
            main.Actions.AddSeparator();
            main.Actions.Add(Actions.Quit.ActionID);

            file.Actions.Add(Actions.Close.ActionID);

            edit.Actions.Add("mac_undo");
            edit.Actions.Add("mac_redo");
            edit.Actions.AddSeparator();
            edit.Actions.Add("mac_cut");
            edit.Actions.Add("mac_copy");
            edit.Actions.Add("mac_paste");
            edit.Actions.Add("mac_delete");
            edit.Actions.AddSeparator();
            edit.Actions.Add("mac_selectAll");

            window.Actions.Add("mac_performMiniaturize");
            window.Actions.Add("mac_performZoom");

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
            ) { ToolBarText = "Start downloads", TooltipText = "Starts downloading all files.", ToolBarItemStyle = "toolbarItemBezel" };

            //limit download speed button
            icon = ResourceHelper.LoadIconFromBundle("toolbar_speed_limit.png");
            ButtonAction limitDownloadSpeedDownloadsTB = new ButtonAction(
                "tb_limit_download_speed",
                "Limit download speed",
                icon,
                new EventHandler<EventArgs>(tbThrottleDownloads_Clicked)
            ) { ToolBarText = "Limit speed", TooltipText = "Limit download speed", ToolBarItemStyle = "toolbarItemBezel" };
            
            //add link
            icon = ResourceHelper.LoadIconFromBundle("toolbar_add.png");
            ButtonAction addURLTB = new ButtonAction(
                "tb_add_url",
                "Add URL",
                icon,
                new EventHandler<EventArgs>(tbAddURL_Clicked)
            ) { ToolBarText = "Add URL", TooltipText = "Add URL to download", ToolBarItemStyle = "toolbarItemBezel" };

            // add action
            addAction(args, startDownloadsTB);
            addAction(args, limitDownloadSpeedDownloadsTB);
            addAction(args, addURLTB);


            //spacer


            //search field
//            SearchAction searchTB = new SearchAction(
//                "tb_filter_downloads",
//                "Filter downloads",
//                new EventHandler<EventArgs>(tbFilterDownloads_Clicked)
//                ) { ToolBarText = "Filter download list", TooltipText = "Filter download list", ToolBarItemStyle = "toolbarSearchField" };

            //addAction(args, addURLTB);

            this.ToolBar = args.ToolBar.GenerateToolBar();
            this.ToolBar.Style = "toolbar";
        }

        private void addAction(GenerateActionArgs args, Eto.Forms.BaseAction action) {
            args.Actions.Add(action);
            args.ToolBar.Add(action.ID);
        }

        public void OnLoadComplete(object sender, EventArgs e) {
            ExpandAllSidebarItems();
        }

        private void ExpandAllSidebarItems() {
            NSOutlineView view = ((NSOutlineView)sidebar.ControlObject);
            
            //TODO: ugly hack
            for (int x = 0; x < view.RowCount; x++) {
                view.ExpandItem(view.ItemAtRow(x), true);
            }

            // view.ItemAtRow(0)
        }
    }
}
