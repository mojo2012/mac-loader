using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace MacLoader {
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController {
		#region Constructors
		
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
			
			SidebarItem downloadsRoot = new SidebarItem("DOWNLOADS");
			downloadsRoot.IsHeader = true;
			rootItems.Add(downloadsRoot);

			downloadsRoot.Children.Add(new SidebarItem("All"));
			downloadsRoot.Children.Add(new SidebarItem("Downloading"));
			downloadsRoot.Children.Add(new SidebarItem("Completed"));
			downloadsRoot.Children.Add(new SidebarItem("Active"));
			downloadsRoot.Children.Add(new SidebarItem("Inactive"));
			
			SidebarItem labelsRoot = new SidebarItem("LABELS");
			labelsRoot.IsHeader = true;
			rootItems.Add(labelsRoot);

			labelsRoot.Children.Add(new SidebarItem("No Label"));
			
			sidebar.DataSource = new SidebarDataSource(rootItems);
			sidebar.Delegate = new SidebarDelegate();
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

