using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Com.Shelinc.SceneTransitionEffects.Modules.SceneTransitionEffects.Implementation  {
	public class FakeSceneTransition : MonoBehaviour, ISceneTransition  {
		public const string ResourcePath = "SceneTransitions/FakeSceneTransition";
		
		public UniTask HideScenes() => UniTask.CompletedTask;

		public UniTask ShowScenes() => UniTask.CompletedTask;
	}
}