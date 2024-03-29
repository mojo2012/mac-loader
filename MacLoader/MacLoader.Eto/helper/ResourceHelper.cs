using System;
using MonoMac.AppKit;
using MonoMac.Foundation;
using System.IO;
using Eto.Drawing;

namespace MacLoader.Helpers {
	public static class ResourceHelper {
		static string resourcesPath = NSBundle.MainBundle.ResourceUrl.Path;
		
		public static NSImage LoadNSImageFromBundle(String fileName) {
			return new NSImage(Path.Combine(resourcesPath, "resources", fileName));
		}

		public static Icon LoadIconFromBundle(String fileName) {
            return new Icon(Path.Combine(resourcesPath, "resources", fileName));
		}
	}
}

