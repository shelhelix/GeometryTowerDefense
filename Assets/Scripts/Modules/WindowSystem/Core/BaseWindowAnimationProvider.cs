using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Com.Shelinc.WindowSystem.Modules.WindowSystem.Core {
	public abstract class BaseWindowAnimationProvider : MonoBehaviour {
		public abstract UniTask StartShowAnimation(CancellationToken token);

		public abstract UniTask StartHideAnimation(CancellationToken token);

		public abstract void HideWindowImmediately();
	}
}