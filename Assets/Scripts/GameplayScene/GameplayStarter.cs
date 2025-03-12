using System.Collections.Generic;
using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using GameComponentAttributes.Attributes;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class GameplayStarter : MonoBehaviour {
		[NotNullReference] public List<AudioClip> Bgms;

		[Inject]
		public void Init(BgmManager bgmManager) {
			bgmManager.PlayBgms(Bgms);
			
			
		}
	}
}