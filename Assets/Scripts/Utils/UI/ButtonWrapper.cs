using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using GameComponentAttributes.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace Game.Utils.UI {
	[RequireComponent(typeof(Button))]
	public sealed class ButtonWrapper : MonoBehaviour {
		[NotNullReference] public Button    Button;
		
		public AudioClip ClickSound;

		AudioPlayer _player;

		void OnValidate() {
			if ( !Button ) {
				Button = GetComponent<Button>();
			}
		}

		[Inject]
		public void Init(AudioPlayer player) {
			_player = player;
		}
		
		public void RemoveAllListeners() {
			Button.onClick.RemoveAllListeners();
			Button.onClick.AddListener(PlaySound);
		}

		public void RemoveAllAndAddListener(UnityAction action) {
			RemoveAllListeners();
			Button.onClick.AddListener(action);
		}

		void PlaySound() {
			if ( !ClickSound ) {
				return;
			}
			_player.PlaySfx(ClickSound);
		}
	}
}