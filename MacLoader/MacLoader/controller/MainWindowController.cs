using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.IO;
using MacLoader.UI;
using MacLoader.UI.widget;
using MacLoader.UI.events;

namespace MacLoader {
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController {
		#region Constructors
		
		static string resourcesPath = NSBundle.MainBundle.ResourceUrl.Path;
		
		// Called when created from unmanaged code
		public MainWindowController(IntPtr handle) : base (handle) {
			Initialize();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base (coder) {
			Initialize();
		}
		
		// Call to load from the XIB/NIB file
		public MainWindowController() : base ("MainWindow") {
			Initialize();
		}
		
		// Shared initialization code
		void Initialize() {
		}
		
		#endregion
		
		#region properties
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}
		#endregion
		
		/// <summary>
		/// User interface loaded from nib file.
		/// </summary>
		public override void AwakeFromNib() {
			SetupUserInterface();
		}
		
		public void SetupUserInterface() {
			//downloadFilterBox.Changed += (sender, e) => {
				
			//};
			
			//setup events
			startDownloadButton.Activated += startDownloadButtonClicked;
			stopDownloadButton.Activated += stopDownloadButtonClicked;
			addURLButton.Activated += addURLButtonClicked;
			downloadFilterBox.Changed += downloadFilterBoxChanged;
			
			SetupSidebar();
			
			//downloadList.DataSource = new DownloadListDataSource();
			//downloadList.Delegate = new DownloadListDelegate();
			
			splitView.Delegate = new SplitViewDelegate();
		}
		
		void SetupSidebar() {
			List<UISourceListItem> rootItems = new List<UISourceListItem>();
			
			UISourceListItem downloadsRoot = new UISourceListItem("Downloads");
			downloadsRoot.IsHeader = true;
			rootItems.Add(downloadsRoot);
			
			
			NSImage iconAll = new NSImage(Path.Combine(resourcesPath, "resources", "status-downloading.png"));
			NSImage iconDownloading = new NSImage(Path.Combine(resourcesPath, "resources", "status-downloading.png"));
			NSImage iconCompleted = new NSImage(Path.Combine(resourcesPath, "resources", "status-completed.png"));
			NSImage iconPaused = new NSImage(Path.Combine(resourcesPath, "resources", "status-paused.png"));
			
			//icon.Size = new System.Drawing.SizeF(16f, 16f);
			
			downloadsRoot.Children.Add(new UISourceListItem("All", iconAll));
			
			downloadsRoot.Children.Add(new UISourceListItem("Downloading", iconDownloading));
			downloadsRoot.Children.Add(new UISourceListItem("Completed", iconCompleted));
			downloadsRoot.Children.Add(new UISourceListItem("Paused", iconPaused));
			
//			SidebarItem i = new SidebarItem("Inactive");
//			i.Children.Add(new SidebarItem("----"));
//			downloadsRoot.Children.Add(i);
			
			UISourceListItem labelsRoot = new UISourceListItem("Labels");
			labelsRoot.IsHeader = true;
			rootItems.Add(labelsRoot);

			labelsRoot.Children.Add(new UISourceListItem("No Label"));
			
//			NSCell cell = new NSImageAndTextCell();
			
//			sidebar.OutlineTableColumn.DataCell = cell;
//			sidebar.DataSource = new SidebarDataSource(rootItems);
//			sidebar.Delegate = new SidebarDelegate();
//			sidebar.Font = NSFont.SystemFontOfSize(NSFont.SmallSystemFontSize);
//			sidebar.FloatsGroupRows = false;
			
			UISourceList sidebarView = new UISourceList(sidebar);
			sidebarView.Items = rootItems;
			sidebarView.ExpandAllItems();
			
			sidebarView.SelectionChanged += sidebarSelectionChanged;
		}
		
		void sidebarSelectionChanged(object sender, UISourceListEventArgs e) {
			System.Console.Out.WriteLine("sidebar: " + e.SelectedItem.Name);
		}
		
		void downloadFilterBoxChanged(object sender, EventArgs e) {
			System.Console.Out.WriteLine("downloadFilterBoxChanged");
		}
		
		void startDownloadButtonClicked(object sender, EventArgs e) {
			System.Console.Out.WriteLine("startDownloadButtonClicked");
		}
		
		void stopDownloadButtonClicked(object sender, EventArgs e) {
			System.Console.Out.WriteLine("stopDownloadButtonClicked");
		}

		void addURLButtonClicked(object sender, EventArgs e) {
			System.Console.Out.WriteLine("addURLButtonClicked");
		}
	}
}

