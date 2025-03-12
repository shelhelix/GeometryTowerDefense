namespace Game.GlobalContext.Sound {
	public class MixerEntry {
		public MixerParamName MixerParamName;
		public float          Volume;

		public MixerEntry(MixerParamName name, float volume) {
			MixerParamName = name;
			Volume         = volume;
		}
	}
}