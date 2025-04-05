using System.Collections.Generic;
using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class GameplayStarter : MonoBehaviour {
		[Required] public List<AudioClip> Bgms;
		[Required] public MapAnimator  mapAnimator;

		[Required] public List<MapLayer> Layers;

		[Inject]
		public void Init(BgmManager bgmManager, CurrencyManager currencyManager) {
			foreach ( var layer in Layers ) {
				layer.Init();
			}
			bgmManager.PlayBgms(Bgms);
			mapAnimator.ShowField().Forget();
			currencyManager.AddGold(100);
		}
	}
}