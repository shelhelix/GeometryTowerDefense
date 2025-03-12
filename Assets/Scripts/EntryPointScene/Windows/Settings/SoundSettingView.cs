using Game.GlobalContext.Sound;
using GameComponentAttributes.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.EntryPointScene.Windows.Settings {
	public class SoundSettingView : MonoBehaviour {
		[NotNullReference] public Slider Slider;

		SoundSettingsController _controller;
		MixerParamName           _mixerParamName;
		
		public void Init(SoundSettingsController settingsController, MixerParamName paramName) {
			_mixerParamName = paramName;
			_controller     = settingsController;
			Slider.onValueChanged.RemoveAllListeners();
			Slider.onValueChanged.AddListener(OnSliderValueChanged);
			Slider.value = _controller.GetNormalizedVolume(_mixerParamName);
		}

		void OnSliderValueChanged(float value) {
			_controller.SetNormalizedVolume(_mixerParamName, value);	
		}
	}
}