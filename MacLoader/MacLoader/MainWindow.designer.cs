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
		MonoMac.AppKit.NSView downloadListView { get; set; }

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
		MonoMac.AppKit.NSTableView downloadList { get; set; }

		[Outlet]
		MonoMac.AppKit.NSSearchField downloadFilterBox { get; set; }

		[Outlet]
		MacLoader.MainWindow mainWindow { get; set; }

		[Outlet]
		MonoMac.AppKit.NSToolbar mainWindowToolbar { get; set; }

		[Outlet]
		MonoMac.AppKit.NSOutlineView sidebar { get; set; }
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
	}
}
