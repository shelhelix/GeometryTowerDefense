using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Game.GameplayScene.Waves {
	public class MonsterSpawnClipAsset : PlayableAsset, ITimelineClipAsset {
		public ClipCaps clipCaps => ClipCaps.None;

		public MonsterSpawnClipBehavior Data;
		
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) => ScriptPlayable<MonsterSpawnClipBehavior>.Create(graph, Data);
	}
}