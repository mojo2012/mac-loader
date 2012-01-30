using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace MacLoader {
	public partial class AppDelegate : NSApplicationDelegate {
		MainWindowController mainWindowController;
		
		/// <summary>
		/// The static entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		static void Main(string [] args) {
			NSApplication.Init();
			NSApplication.Main(args);
		}
		
		public AppDelegate() {
		}
		
		
		/// <summary>
		/// Loads the main UI controller
		/// </summary>
		/// <param name='notification'>
		/// Notification.
		/// </param>
		public override void FinishedLaunching(NSObject notification) {
			mainWindowController = new MainWindowController();
			mainWindowController.Window.MakeKeyAndOrderFront(this);
		}
	}
}

