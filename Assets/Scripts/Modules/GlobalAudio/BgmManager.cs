using System.Collections.Generic;
using UnityEngine;

namespace Com.Shelinc.GlobalAudio.Modules.GlobalAudio {
	public class BgmManager {
		readonly AudioPlayer _player;

		List<AudioClip> _activeClips;

		int  _currentClipIndex;
		bool _loopClips;

		public BgmManager(AudioPlayer player) {
			_player              =  player;
			_player.BgmClipEnded += OnBgmEnded;
		}

		public void PlayBgms(List<AudioClip> clips, bool loop = true) {
			_activeClips      = clips;
			_loopClips        = loop;
			_currentClipIndex = 0;
			PlayCurrentClip();
		}

		public void PlayNextSong() {
			if ( _activeClips.Count == 0 ) {
				return;
			}
			if ( !IsValidIndex(_currentClipIndex) ) {
				Debug.LogError("Can't start next BGM. Index is invalid");
				return;
			}	
			if ( !_loopClips && !IsValidIndex(_currentClipIndex + 1) ) {
				_currentClipIndex = -1;
				return;
			}
			_currentClipIndex = (_currentClipIndex + 1) % _activeClips.Count;
			PlayCurrentClip();
		}

		void PlayCurrentClip() {
			if ( _activeClips.Count == 0 ) {
				return;
			}
			if ( !IsValidIndex(_currentClipIndex) ) {
				Debug.LogError("Can't play current clip cause index is out of range");
				return;
			}	
			var clip = _activeClips[_currentClipIndex];
			_player.PlayBgm(clip, false);
		}

		void OnBgmEnded() {
			PlayNextSong();
		}
		
		bool IsValidIndex(int index) => (index >= 0) && (index < _activeClips.Count);
	}
}