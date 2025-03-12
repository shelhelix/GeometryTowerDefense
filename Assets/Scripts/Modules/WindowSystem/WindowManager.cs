using System.Collections.Generic;
using System.Linq;
using Com.Shelinc.WindowSystem.Modules.WindowSystem.Core;
using GameComponentAttributes.Attributes;
using UnityEngine;

namespace Com.Shelinc.WindowSystem.Modules.WindowSystem {
	public class WindowManager : MonoBehaviour {
		[NotNullReference] public List<BaseWindow> Windows;
		
		List<BaseWindow> _windowStack = new();

		void Start() {
			foreach ( var window in Windows ) {
				window.HideFinished.Subscribe(() => OnWindowHidden(window));
				window.HideImmediately();
				window.gameObject.SetActive(false);
			}	
		}
		
		// start show and return window
		public BaseWindow ShowWindow(IShowRequest showRequest) {
			var hasWindowInStack = _windowStack.Any(window => window.GetType() == showRequest.WindowType);
			if ( hasWindowInStack ) {
				Debug.LogWarning($"Already showing window {showRequest.WindowType}. You need to close this window first before showing it again");
				return null;
			}
			var window = Windows.Find(window => window.GetType() == showRequest.WindowType);
			if ( window == null ) {
				Debug.LogError($"Can't start window {showRequest.WindowType}. Window not found in a list");
				return null;
			}
			window.gameObject.SetActive(true);
			window.Show();
			_windowStack.Add(window);
			return window;
		}

		void OnWindowHidden(BaseWindow window) {
			if ( _windowStack.Count == 0 ) {
				return;
			}
			if ( _windowStack.Last() != window ) {
				Debug.LogWarning("Something wrong. Window that was hidden is not the top window in stack. Please check your logic");
			}
			window.gameObject.SetActive(false);
			_windowStack.Remove(window);
		}
	}
}