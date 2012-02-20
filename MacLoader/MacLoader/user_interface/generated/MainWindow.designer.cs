// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace MacLoader
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSTextField statusLabel { get; set; }

		[Outlet]
		MonoMac.AppKit.NSOutlineView sidebar { get; set; }

		[Outlet]
		MonoMac.AppKit.NSOutlineView downloadList { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView sidebarView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSplitView splitView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton stopDownloadButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton addURLButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton startDownloadButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSearchField downloadFilterBox { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbar mainWindowToolbar { get; set; }

		[Outlet]
		MonoMac.AppKit.NSView analyzeURLPopoverView { get; set; }

		[Outlet]
		MonoMac.AppKit.NSPopover analyzeURLPopover { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (statusLabel != null) {
				statusLabel.Dispose ();
				statusLabel = null;
			}

			if (sidebar != null) {
				sidebar.Dispose ();
				sidebar = null;
			}

			if (downloadList != null) {
				downloadList.Dispose ();
				downloadList = null;
			}

			if (sidebarView != null) {
				sidebarView.Dispose ();
				sidebarView = null;
			}

			if (splitView != null) {
				splitView.Dispose ();
				splitView = null;
			}

			if (stopDownloadButton != null) {
				stopDownloadButton.Dispose ();
				stopDownloadButton = null;
			}

			if (addURLButton != null) {
				addURLButton.Dispose ();
				addURLButton = null;
			}

			if (startDownloadButton != null) {
				startDownloadButton.Dispose ();
				startDownloadButton = null;
			}

			if (downloadFilterBox != null) {
				downloadFilterBox.Dispose ();
				downloadFilterBox = null;
			}

			if (mainWindowToolbar != null) {
				mainWindowToolbar.Dispose ();
				mainWindowToolbar = null;
			}

			if (analyzeURLPopoverView != null) {
				analyzeURLPopoverView.Dispose ();
				analyzeURLPopoverView = null;
			}

			if (analyzeURLPopover != null) {
				analyzeURLPopover.Dispose ();
				analyzeURLPopover = null;
			}
		}
	}
}
