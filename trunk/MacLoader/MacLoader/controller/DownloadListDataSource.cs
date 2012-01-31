using System;
using System.Collections.Generic;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace MacLoader {
	public class DownloadListDataSource : NSTableViewDataSource {
		List<DownloadListItem> downloadListItems = null;
		
		public DownloadListDataSource() {
			downloadListItems = new List<DownloadListItem>();
			
//			DownloadListItem item = new DownloadListItem();
//			item.FileName = "Test";
//			item.Hoster = "Netload.in";
//			item.Progress = 30;
			
//			downloadListItems.Add(item);
			
		}
	
		public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, int row) {
			String retVal = "";
			
			switch (tableColumn.HeaderCell.StringValue) {
			case "Filename":
				retVal = downloadListItems[row].FileName;
				break;
			case "Hoster":
				retVal = downloadListItems[row].Hoster;
				break;
			case "Progress":
				retVal = downloadListItems[row].Progress.ToString();
				break;
			}
			
			return new NSString(retVal);
		}

		public override int GetRowCount(NSTableView tableView) {
			return downloadListItems.Count;
		}
	}
	
	public class DownloadListItem : NSObject {
		String filename = "";
		String hoster = "";
		int progress = 0;
		bool finished = false;

		public String FileName {
			get {
				return this.filename;
			}
			set {
				filename = value;
			}
		}

		public bool Finished {
			get {
				return this.finished;
			}
			set {
				finished = value;
			}
		}

		public String Hoster {
			get {
				return this.hoster;
			}
			set {
				hoster = value;
			}
		}

		public int Progress {
			get {
				return this.progress;
			}
			set {
				progress = value;
			}
		}
	}
	
	public class DownloadListDelegate : NSTableViewDelegate {
		public override float GetRowHeight(NSTableView tableView, int row) {
			return 20f;
		}
	
	}
}

