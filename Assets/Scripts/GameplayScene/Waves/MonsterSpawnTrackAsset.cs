using UnityEngine.Timeline;

namespace Game.GameplayScene.Waves {
	[TrackColor(0, 1, 1)]
	[TrackBindingType(typeof(MonsterSpawner))]
	[TrackClipType(typeof(MonsterSpawnClipAsset))]
	public class MonsterSpawnTrackAsset : TrackAsset {
		
	}
}