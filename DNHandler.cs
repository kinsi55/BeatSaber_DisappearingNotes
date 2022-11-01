using System.Collections;
using DisappearingNotes;
using SiraUtil.Submissions;
using UnityEngine;
using Zenject;

namespace Installers {
	internal class DNHandler {
		const int NormalNoteLayer = 8;
		public const int DesktopOnlyLayer = 23;

		public static bool hasVrCamera = false;

		public DNHandler(Submission submission, MainCamera mainCamera) {
			hasVrCamera = mainCamera.camera.stereoEnabled;

			if(!Config.Instance.enableInVR && hasVrCamera) {
				mainCamera.camera.cullingMask &= ~(1 << DesktopOnlyLayer);

				SharedCoroutineStarter.instance.StartCoroutine(InitStuff());
			} else {
				submission.DisableScoreSubmission("Disappearing Notes");
			}
		}

		IEnumerator InitStuff() {
			yield return null;

			int toggle(int flag, bool show = true) => show ? flag | 1 << DesktopOnlyLayer : flag & ~(1 << DesktopOnlyLayer);

			foreach(var cam in Resources.FindObjectsOfTypeAll<Camera>()) {
				if(cam.name == "MainCamera") {
					cam.cullingMask = toggle(cam.cullingMask, false);
				} else {
					cam.cullingMask = toggle(cam.cullingMask, (cam.cullingMask & (1 << NormalNoteLayer)) != 0);
				}
			}

			var LIVTHING = Object.FindObjectOfType<LIV.SDK.Unity.LIV>();

			if(LIVTHING != null)
				LIVTHING.spectatorLayerMask = toggle(LIVTHING.spectatorLayerMask, false);
		}
	}
}