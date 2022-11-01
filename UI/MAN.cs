using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using HMUI;

namespace DisappearingNotes.UI {
	class MAN {
		Config config = Config.Instance;

		readonly string version = $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString(3)} by Kinsi55\nCommissioned by LackWiz";

		[UIComponent("sponsorsText")] CurvedTextMeshPro sponsorsText = null;
		void OpenSponsorsLink() => Process.Start("https://github.com/sponsors/kinsi55");
		async void OpenSponsorsModal() {
			sponsorsText.text = "Loading...";
			var desc = await Task.Run(() => {
				try {
					return (new WebClient()).DownloadString("http://kinsi.me/sponsors/bsout.php");
				} catch { }
				return "Failed to load";
			});

			sponsorsText.text = desc;
			// There is almost certainly a better way to update / correctly set the scrollbar size...
			sponsorsText.gameObject.SetActive(false);
			sponsorsText.gameObject.SetActive(true);
		}
	}
}
