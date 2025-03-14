using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayScene {
	public class MonsterSpawner : MonoBehaviour {
		public Transform EndPoint;
		public Monster   MonsterPrefab;
		
		public float SpawnInterval;

		float _leftTime;
		
		void Update() {
			if ( _leftTime < 0 ) {
				var monster = Instantiate(MonsterPrefab);
				monster.Init(new List<Vector3> { transform.position, EndPoint.position});
				_leftTime = SpawnInterval;
			}
			_leftTime -= Time.deltaTime;
		}
		
	}
}