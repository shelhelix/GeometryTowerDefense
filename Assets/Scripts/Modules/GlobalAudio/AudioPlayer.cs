using System;
using UnityEngine;

namespace Com.Shelinc.GlobalAudio.Modules.GlobalAudio {
	public class AudioPlayer {
		AudioSource _sfxSource;
		AudioSource _bgmSource;
		
		AudioSourceWatcher _bgmWatcher;

		public event Action BgmClipEnded;

		[ClearOnReload]
		public static int Counter;
		
		public AudioPlayer(AudioElements audioElements) {
			_sfxSource                    =  audioElements.SfxPlayer;
			_bgmSource                    =  audioElements.BgmPlayer;
			_bgmWatcher                   =  new AudioSourceWatcher(_bgmSource);
			_bgmWatcher.IsFinishedPlaying += OnBgmFinished;
		}
		
		public void PlaySfx(AudioClip clip) {
			_sfxSource.PlayOneShot(clip);
		}
		
		public void PlayBgm(AudioClip clip, bool loop = true) {
			_bgmWatcher.StopWatching();
			_bgmSource.clip = clip;
			_bgmSource.loop = loop;
			_bgmSource.Play();
			_bgmWatcher.StartWatching();
		}

		void OnBgmFinished() {
			BgmClipEnded?.Invoke();
		}
	}
}