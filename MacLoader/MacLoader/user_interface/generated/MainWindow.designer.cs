// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace MacLoader {
	[Register ("MainWindow")]
	partial class MainWindow {
		[Outlet]
		public MonoMac.AppKit.NSView templateSidebarEntry { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSTextField statusLabel { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSOutlineView sidebar { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSOutlineView downloadList { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSView sidebarView { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSSplitView splitView { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSButton stopDownloadButton { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSButton addURLButton { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSButton startDownloadButton { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSSearchField downloadFilterBox { get; set; }

		[Outlet]
		public MacLoader.MainWindow mainWindow { get; set; }

		[Outlet]
		public MonoMac.AppKit.NSToolbar mainWindowToolbar { get; set; }
		
		void ReleaseDesignerOutlets() {
			if (templateSidebarEntry != null) {
				templateSidebarEntry.Dispose();
				templateSidebarEntry = null;
			}

			if (statusLabel != null) {
				statusLabel.Dispose();
				statusLabel = null;
			}

			if (sidebar != null) {
				sidebar.Dispose();
				sidebar = null;
			}

			if (downloadList != null) {
				downloadList.Dispose();
				downloadList = null;
			}

			if (sidebarView != null) {
				sidebarView.Dispose();
				sidebarView = null;
			}

			if (splitView != null) {
				splitView.Dispose();
				splitView = null;
			}

			if (stopDownloadButton != null) {
				stopDownloadButton.Dispose();
				stopDownloadButton = null;
			}

			if (addURLButton != null) {
				addURLButton.Dispose();
				addURLButton = null;
			}

			if (startDownloadButton != null) {
				startDownloadButton.Dispose();
				startDownloadButton = null;
			}

			if (downloadFilterBox != null) {
				downloadFilterBox.Dispose();
				downloadFilterBox = null;
			}

			if (mainWindow != null) {
				mainWindow.Dispose();
				mainWindow = null;
			}

			if (mainWindowToolbar != null) {
				mainWindowToolbar.Dispose();
				mainWindowToolbar = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController {
		
		void ReleaseDesignerOutlets() {
		}
	}
}
