using System.Collections.Generic;
using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using Com.Shelinc.WindowSystem.Modules.WindowSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.EntryPointScene.Windows.Settings;
using Game.GlobalContext;
using Game.Utils;
using Game.Utils.UI;
using GameComponentAttributes.Attributes;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace Game.EntryPointScene {
	public class EntryPointStarter : MonoBehaviour {
		const string JamLink = "add link to jam here";
		
		[NotNullReference] public List<AudioClip> Bgms;

		// canvas group for animation
		[NotNullReference] public CanvasGroup Group;
		
		[NotNullReference] public ButtonWrapper PlayButton;
		[NotNullReference] public ButtonWrapper SettingsButton;
		[NotNullReference] public ButtonWrapper JamLinkButton;
		[NotNullReference] public ButtonWrapper ExitButton;

		[Inject]
		public void Init(BgmManager bgmManager, SceneLoader sceneLoader, WindowManager windowManager) {
			bgmManager.PlayBgms(Bgms);
			PlayButton.RemoveAllAndAddListener(() => sceneLoader.LoadScene(SceneLoader.GameplayScene).Forget());
			SettingsButton.RemoveAllAndAddListener(() => ShowSettingsWindow(windowManager).Forget());
			JamLinkButton.RemoveAllAndAddListener(() => Application.OpenURL(JamLink));
			ExitButton.RemoveAllAndAddListener(Exit);
			// Application.quit doesn't work in web builds
			ExitButton.gameObject.SetActive(!PlatformDetector.IsWeb);
		}

		async UniTask ShowSettingsWindow(WindowManager windowManager) {
			Group.interactable = false;
			var hidingTask  = Group.DOFade(0, 0.3f).ToUniTask();
			var shownWindow = windowManager.ShowWindow(SettingsWindow.CreateShowRequest());
			await shownWindow.HideStarted.AwaitTrigger();
			await hidingTask;
			// return to normal after window and main screen view is hidden
			Group.interactable = true;
			await Group.DOFade(1, 0.3f).ToUniTask();
		}
		
		void Exit() {
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}