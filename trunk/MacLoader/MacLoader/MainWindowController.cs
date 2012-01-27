using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.IO;

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
		
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}
		
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
			
			downloadList.DataSource = new DownloadListDataSource();
			downloadList.Delegate = new DownloadListDelegate();
			
			splitView.Delegate = new SplitViewDelegate();
		}
		
		void SetupSidebar() {
			List<SidebarItem> rootItems = new List<SidebarItem>();
			
			SidebarItem downloadsRoot = new SidebarItem("Downloads");
			downloadsRoot.IsHeader = true;
			rootItems.Add(downloadsRoot);
			
			
			NSImage iconAll = new NSImage(Path.Combine(resourcesPath, "resources", "status-downloading.png"));
			NSImage iconDownloading = new NSImage(Path.Combine(resourcesPath, "resources", "status-downloading.png"));
			NSImage iconCompleted = new NSImage(Path.Combine(resourcesPath, "resources", "status-completed.png"));
			NSImage iconPaused = new NSImage(Path.Combine(resourcesPath, "resources", "status-paused.png"));
			
			//icon.Size = new System.Drawing.SizeF(16f, 16f);
			
			downloadsRoot.Children.Add(new SidebarItem("All", iconAll));
			
			downloadsRoot.Children.Add(new SidebarItem("Downloading", iconDownloading));
			downloadsRoot.Children.Add(new SidebarItem("Completed", iconCompleted));
			downloadsRoot.Children.Add(new SidebarItem("Paused", iconPaused));
			
//			SidebarItem i = new SidebarItem("Inactive");
//			i.Children.Add(new SidebarItem("----"));
//			downloadsRoot.Children.Add(i);
			
			SidebarItem labelsRoot = new SidebarItem("Labels");
			labelsRoot.IsHeader = true;
			rootItems.Add(labelsRoot);

			labelsRoot.Children.Add(new SidebarItem("No Label"));
			
			NSCell cell = new NSImageAndTextCell();
			
			sidebar.OutlineTableColumn.DataCell = cell;
			sidebar.DataSource = new SidebarDataSource(rootItems);
			sidebar.Delegate = new SidebarDelegate();
			sidebar.Font = NSFont.SystemFontOfSize(NSFont.SmallSystemFontSize);
			sidebar.FloatsGroupRows = false;
			
			for (int x=0; x <sidebar.RowCount; x++) {
				sidebar.ExpandItem(sidebar.ItemAtRow(x), true);
			}
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

