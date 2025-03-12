using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Com.Shelinc.WindowSystem.Modules.WindowSystem.Utils {
	public class EventWrapper {
		List<Action> _subscribers = new();
		
		public void Subscribe(Action callback) {
			if ( callback == null ) {
				return;
			}
			_subscribers.Add(callback);
		}
		
		public void Unsubscribe(Action callback) {
			_subscribers.Remove(callback);
		}

		public async UniTask AwaitTrigger() {
			var completionSource = new UniTaskCompletionSource();
			Subscribe(SetResult);
			await completionSource.Task;
			Unsubscribe(SetResult);
			return;

			void SetResult() => completionSource.TrySetResult();
		}

		public void Fire() {
			var subscribersCopy = new List<Action>(_subscribers);
			foreach ( var subscriber in subscribersCopy ) {
				subscriber();
			}
		}
	}
}