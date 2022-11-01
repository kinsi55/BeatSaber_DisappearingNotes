using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using SiraUtil.Affinity;
using UnityEngine;

namespace DisappearingNotes.HarmonyPatches {
	class GhostNoteMeshFader : IAffinity {
		static readonly IPA.Utilities.FieldAccessor<DisappearingArrowController, GameNoteController>.Accessor DisappearingArrowController_gameNoteController =
			IPA.Utilities.FieldAccessor<DisappearingArrowController, GameNoteController>.GetAccessor("_gameNoteController");

		static readonly IPA.Utilities.FieldAccessor<CutoutAnimateEffect, CutoutEffect[]>.Accessor CutoutAnimateEffect_gameNoteController =
			IPA.Utilities.FieldAccessor<CutoutAnimateEffect, CutoutEffect[]>.GetAccessor("_cuttoutEffects");


		[AffinityPatch(typeof(DisappearingArrowControllerBase<GameNoteController>), "HandleNoteMovementNoteDidMoveInJumpPhase")]
		[AffinityPostfix]
		void HandleNoteMovementNoteDidMoveInJumpPhase(DisappearingArrowControllerBase<GameNoteController> __instance) {
			// This is kind of not amazing
			if(!(__instance is DisappearingArrowController dac))
				return;

			var dist = DisappearingArrowController_gameNoteController(ref dac).noteMovement.distanceToPlayer;

			if(dist < Config.Instance.fadeEndDistance)
				return;

			var cutoutAnimateEffect = __instance.gameObject.GetComponent<CutoutAnimateEffect>();

			if(cutoutAnimateEffect == null)
				return;

			var _cuttoutEffects = CutoutAnimateEffect_gameNoteController(ref cutoutAnimateEffect);

			foreach(var cae in _cuttoutEffects) {
				if(cae.name != "NoteCube")
					continue;

				cae.SetCutout(1f - Mathf.Clamp01((dist - Config.Instance.fadeEndDistance) / Config.Instance.fadeDistanceDuration));

				break;
			}
		}
	}
}
