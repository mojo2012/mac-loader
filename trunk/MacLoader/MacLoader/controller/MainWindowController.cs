using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;
using System.IO;
using MacLoader.UI;
using MacLoader.UI.widget;
using MacLoader.UI.events;
using MacLoader.Helpers;

namespace MacLoader {
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController {
		#region fields
		
		UIPopover popover = null;
		#endregion
				
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
			
			splitView.Delegate = new SplitViewDelegate();
			
			popover = new UIPopover(analyzeURLPopoverView);
		}
		
		void SetupSidebar() {
			List<UISourceListItem> rootItems = new List<UISourceListItem>();
			
			UISourceListItem downloadsRoot = new UISourceListItem("Downloads");
			downloadsRoot.IsHeader = true;
			rootItems.Add(downloadsRoot);
			
			
			NSImage iconAll = ResourceHelper.LoadImageFromBundle("status-downloading.png");
			NSImage iconDownloading = ResourceHelper.LoadImageFromBundle("status-downloading.png");
			NSImage iconCompleted = ResourceHelper.LoadImageFromBundle("status-completed.png");
			NSImage iconPaused = ResourceHelper.LoadImageFromBundle("status-paused.png");
			
			downloadsRoot.Children.Add(new UISourceListItem("All", iconAll));
			downloadsRoot.Children.Add(new UISourceListItem("Downloading", iconDownloading));
			downloadsRoot.Children.Add(new UISourceListItem("Completed", iconCompleted));
			downloadsRoot.Children.Add(new UISourceListItem("Paused", iconPaused));
			
			
			UISourceListItem linkGrabberRoot = new UISourceListItem("Link Grabber");
			linkGrabberRoot.IsHeader = true;
			rootItems.Add(linkGrabberRoot);
			
			
			NSImage iconLinkGrabberAll = ResourceHelper.LoadImageFromBundle("status-mixed.png");
			NSImage iconLinkAvailable = ResourceHelper.LoadImageFromBundle("status-online.png");
			NSImage iconLinkOffline = ResourceHelper.LoadImageFromBundle("status-not-available.png");
			NSImage iconLinkProcessing = ResourceHelper.LoadImageFromBundle("status-processing.png");
			
			linkGrabberRoot.Children.Add(new UISourceListItem("All", iconLinkGrabberAll));
			linkGrabberRoot.Children.Add(new UISourceListItem("Processing", iconLinkProcessing));
			linkGrabberRoot.Children.Add(new UISourceListItem("Available", iconLinkAvailable));
			linkGrabberRoot.Children.Add(new UISourceListItem("Offline", iconLinkOffline));
						
			UISourceListItem labelsRoot = new UISourceListItem("Labels");
			labelsRoot.IsHeader = true;
			rootItems.Add(labelsRoot);

			labelsRoot.Children.Add(new UISourceListItem("No Label"));
			
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
			if (!popover.Shown) {
				popover.Show(addURLButton.Bounds, (NSView)sender, NSRectEdge.MaxYEdge, true);
			} else {
				popover.Close();
			}
		}
	}
}

