using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.GameplayScene.Waves {
	[Serializable]
	public class MonsterSpawnClipBehavior : PlayableBehaviour {
		public Monster SpawnPrefab;
		public float   SpawnPeriod;
		
		float _timePassed;
		
		public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
			base.ProcessFrame(playable, info, playerData);
			var spawner = playerData as MonsterSpawner;
			_timePassed += info.deltaTime;
			if ( !spawner ) {
				return;
			}
			
			if ( !Application.isPlaying ) {
				return;
			}
			if ( _timePassed <= SpawnPeriod ) {
				return;
			}
			spawner.Spawn(SpawnPrefab);
			_timePassed = 0;
		}
	}
}