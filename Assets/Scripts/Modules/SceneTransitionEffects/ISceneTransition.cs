using Cysharp.Threading.Tasks;

namespace Com.Shelinc.SceneTransitionEffects.Modules.SceneTransitionEffects  {
	public interface ISceneTransition {
		public UniTask HideScenes();
		public UniTask ShowScenes();
	}
}