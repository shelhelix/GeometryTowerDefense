using System.Collections.Generic;
using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class GameplayStarter : MonoBehaviour {
		[Required] public List<AudioClip> Bgms;
		[Required] public MapShowAnimation  mapShowAnimation;

		[Required] public List<MapLayer> Layers;

		[Inject]
		public void Init(BgmManager bgmManager) {
			foreach ( var layer in Layers ) {
				layer.Init();
			}
			bgmManager.PlayBgms(Bgms);
			mapShowAnimation.ShowField().Forget();
		}
	}
}