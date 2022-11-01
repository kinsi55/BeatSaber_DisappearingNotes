using DisappearingNotes.UI;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace DisappearingNotes {
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin {
		internal static Plugin Instance;
		internal static IPALogger Log;
		internal static Harmony harmony;

		[Init]
		public Plugin(IPALogger logger, IPA.Config.Config conf, Zenjector zenjector) {
			Instance = this;
			Log = logger;
			Config.Instance = conf.Generated<Config>();
			harmony = new Harmony("Kinsi55.BeatSaber.DisappearingNotes");

			zenjector.Install<Installers.GameInstaller>(Location.StandardPlayer);

			BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("Disappearing Notes", "DisappearingNotes.UI.modstab.bsml", new MAN());
		}

		[OnEnable]
		public void OnEnable() {
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}

		[OnDisable]
		public void OnDisable() {
			harmony.UnpatchSelf();
		}
	}
}
