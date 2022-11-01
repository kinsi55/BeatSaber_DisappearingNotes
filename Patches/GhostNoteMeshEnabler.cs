using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Installers;
using SiraUtil.Affinity;
using UnityEngine;

namespace DisappearingNotes.HarmonyPatches {
	class GhostNoteMeshEnabler : IAffinity {

		[AffinityPatch(typeof(DisappearingArrowControllerBase<GameNoteController>), "HandleCubeNoteControllerDidInit")]
		[AffinityPostfix]
		void HandleCubeNoteControllerDidInit(MeshRenderer ____cubeMeshRenderer) {
			____cubeMeshRenderer.enabled = true;

			if(!Config.Instance.enableInVR && DNHandler.hasVrCamera)
				____cubeMeshRenderer.gameObject.layer = DNHandler.DesktopOnlyLayer;
		}
	}
}
