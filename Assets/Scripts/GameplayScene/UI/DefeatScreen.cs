using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.GlobalContext;
using Game.Utils.UI;
using TMPEffects.Components;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class DefeatScreen : MonoBehaviour {
		[Required] public ButtonWrapper ReturnToMainSceneButton;
		[Required] public CanvasGroup   ButtonCanvasGroup;
		[Required] public CanvasGroup   Background;
		[Required] public TMPWriter     DefeatTextAppearEffect;
 
		SceneLoader _sceneLoader;

		Sequence _sequence;
		
		[Inject]
		public void Init(SceneLoader sceneLoader) {
			ReturnToMainSceneButton.RemoveAllAndAddListener(() => sceneLoader.LoadScene(SceneLoader.MainMenuScene).Forget());
		}

		public void Show() {
			gameObject.SetActive(true);
			_sequence = DOTween.Sequence()
				.Append(Background.DOFade(1, 0.4f).From(0))
				.AppendCallback(() => DefeatTextAppearEffect.Show(0, 6))
				.AppendInterval(0.6f)
				.Append(ButtonCanvasGroup.DOFade(1, 0.4f).From(0));
		}

		protected void OnDestroy() {
			_sequence?.Kill();
		}
	}
}