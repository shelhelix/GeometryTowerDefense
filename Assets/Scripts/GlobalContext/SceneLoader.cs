using Com.Shelinc.SceneTransitionEffects.Modules.SceneTransitionEffects;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.GlobalContext {
	public class SceneLoader {
		public const string GameplayScene = "Gameplay";
		public const string MainMenuScene = "MainMenu";
		
		readonly ISceneTransition _transition;

		public SceneLoader(ISceneTransition transition) => _transition = transition;

		public async UniTask LoadScene(string sceneName) {
			await _transition.HideScenes();
			await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
			await _transition.ShowScenes();
		}
	}
}