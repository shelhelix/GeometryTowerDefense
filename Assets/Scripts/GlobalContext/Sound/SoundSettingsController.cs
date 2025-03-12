using System;
using AsyncUtils;
using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.GlobalContext.Sound {
	public class SoundSettingsController : IDisposable {
		SoundSettingsControllerPrefsState _controllerPrefsState = new();

		AudioMixer _mixer;
		AsyncTimer _timer;
		
		public SoundSettingsController(AudioElements audioElements) {
			_mixer = audioElements.Mixer;
			// Delayed init cause mixer don't want to accept changes in the first frame
			_timer            =  new AsyncTimer();
			_timer.OnTimerEnd += UpdateAllMixerValues;
			_timer.Start(0.1f, 0.1f);
		}
		
		public void Dispose() {
			_timer.Stop();
		}
		
		public float GetNormalizedVolume(MixerParamName mixerParamName) => _controllerPrefsState.GetOrCreateEntry(mixerParamName).Volume;

		public void SetNormalizedVolume(MixerParamName mixerParamName, float volume) {
			var clampedVolume = Mathf.Clamp01(volume);
			_controllerPrefsState.GetOrCreateEntry(mixerParamName).Volume = clampedVolume;
			_controllerPrefsState.Save();
			var absoluteVolume = CalculateAbsoluteVolume(volume);
			_mixer.GetFloat(mixerParamName.ToString(), out var currentVolume);
			Debug.Log($"Set sound params {mixerParamName}: {currentVolume} db -> {absoluteVolume} db");
			_mixer.SetFloat(mixerParamName.ToString(), absoluteVolume);
		}

		void UpdateAllMixerValues() {
			foreach ( MixerParamName mixerParameter in Enum.GetValues(typeof(MixerParamName)) ) {
				SetNormalizedVolume(mixerParameter, GetNormalizedVolume(mixerParameter));
			}
		}

		float CalculateAbsoluteVolume(float normalizedVolume) => Mathf.Log10(normalizedVolume) * 20;
	}
}