using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameComponentAttributes.Attributes;
using UnityEngine;

namespace Com.Shelinc.WindowSystem.Modules.WindowSystem.Core.Implementation {
	public class FadeWindowAnimationProvider : BaseWindowAnimationProvider {
		[NotNullReference] public CanvasGroup CanvasGroup;

		public float AppearAnimationTime    = 0.4f;
		public float DisappearAnimationTime = 0.3f;
		public Ease  AppearEase             = Ease.OutBack;
		public Ease  DisappearEase          = Ease.InBack;
		
		public override UniTask StartShowAnimation(CancellationToken token) {
			transform.localScale = Vector3.one * 0.2f;
			return UniTask.WhenAll(
				CanvasGroup.DOFade(1, AppearAnimationTime).SetEase(AppearEase).WithCancellation(token),
				transform.DOScale(1, AppearAnimationTime).SetEase(AppearEase).WithCancellation(token)
			);
		}

		public override UniTask StartHideAnimation(CancellationToken token) =>
			UniTask.WhenAll(
				CanvasGroup.DOFade(0f, DisappearAnimationTime).SetEase(DisappearEase).WithCancellation(token),
				transform.DOScale(0f, DisappearAnimationTime).SetEase(DisappearEase).WithCancellation(token)
			);

		public override void HideWindowImmediately() {
			CanvasGroup.alpha = 0;
			transform.localScale = Vector3.zero;
		}
	}
}