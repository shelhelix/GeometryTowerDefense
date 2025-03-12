using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GlobalContext.Sound {
	public class SoundSettingsControllerPrefsState {
		public List<MixerEntry> Entries = new();

		public SoundSettingsControllerPrefsState() {
			foreach ( MixerParamName name in Enum.GetValues(typeof(MixerParamName)) ) {
				GetOrCreateEntry(name).Volume = PlayerPrefs.GetFloat(name.ToString(), 1);
			}
		}

		public void Save() {
			foreach ( var entry in Entries ) {
				PlayerPrefs.SetFloat(entry.MixerParamName.ToString(), entry.Volume);
			}
		}

		public MixerEntry GetOrCreateEntry(MixerParamName mixerParamName) {
			var res = Entries.Find(x => x.MixerParamName == mixerParamName);
			if ( res != null ) {
				return res;
			}
			res = new MixerEntry(mixerParamName, 1);
			Entries.Add(res);
			return res;
		}
	}
}