using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.GameplayScene {
	public class TileAnimator : MonoBehaviour {
		public void InstantHide() {
			transform.localScale = Vector3.zero;
		}
		
		public async UniTask PlayAppearAnimation() {
			var initialScale = Vector3.one;
			await DOTween.Sequence()
				.Append(transform.DOScale(initialScale * 1.2f, .3f))
				.Append(transform.DOScale(initialScale, .3f));	
		}
	}
}