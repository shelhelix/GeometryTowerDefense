using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Com.Shelinc.GlobalAudio.Modules.GlobalAudio {
	class AudioSourceWatcher {
		AudioSource _source;

		CancellationTokenSource _cancellationTokenSource;
		
		public event Action IsFinishedPlaying; 
		
		
		public AudioSourceWatcher(AudioSource source) {
			_source = source;
		}
		
		public void StartWatching() {
			StopWatching();
			_cancellationTokenSource = new CancellationTokenSource();
			WaitAndFireBgmEnd(_cancellationTokenSource.Token).Forget();
		}

		public void StopWatching() {
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = null;
		}

		async UniTask WaitAndFireBgmEnd(CancellationToken cancellationToken) {
			await UniTask.WaitUntil(() => _source && !_source.isPlaying, cancellationToken: cancellationToken);
			if ( cancellationToken.IsCancellationRequested ) {
				return;
			}
			IsFinishedPlaying?.Invoke();
		}
	}
}