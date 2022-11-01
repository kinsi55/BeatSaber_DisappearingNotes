using DisappearingNotes;
using DisappearingNotes.HarmonyPatches;
using Zenject;

namespace Installers {
	internal class GameInstaller : MonoInstaller {
		public override void InstallBindings() {
			if(!Config.Instance.enable)
				return;

			var x = Container.TryResolve<GameplayModifiers>();

			if(x?.ghostNotes != true)
				return;

			Container.Bind<DNHandler>().AsSingle().NonLazy();
			Container.BindInterfacesTo<GhostNoteMeshEnabler>().AsSingle().NonLazy();
			Container.BindInterfacesTo<GhostNoteMeshFader>().AsSingle().NonLazy();
		}
	}
}