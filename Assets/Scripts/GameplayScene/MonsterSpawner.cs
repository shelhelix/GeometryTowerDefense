using UnityEngine;

namespace Game.GameplayScene {
	public class MonsterSpawner : MonoBehaviour {
		public Transform  EndPoint;
		public Monster    MonsterPrefab;
		public Pathfinder Pathfinder;
		
		public float SpawnInterval;

		float _leftTime;
		
		void Update() {
			if ( _leftTime < 0 ) {
				var monster = Instantiate(MonsterPrefab);
				var path = Pathfinder.FindPath(transform, EndPoint);
				monster.Init(path);
				_leftTime = SpawnInterval;
			}
			_leftTime -= Time.deltaTime;
		}
		
	}
}