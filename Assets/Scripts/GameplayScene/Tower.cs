using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameplayScene {
	public class Tower : MonoBehaviour {
		public Transform FireHead;
		public Transform FirePoint;
		
		public Bullet BulletPrefab;
		
		public float FireInterval;

		List<Monster> _monsters = new();

		float _fireTime;
		
		void OnTriggerEnter2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsters.Add(monster);
		}

		void OnTriggerExit2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsters.Remove(monster);
		}

		void Update() {
			if ( _monsters.Count == 0 ) {
				return;
			}
			var monster = _monsters.First();
			// look at 2d object
			FireHead.up = (monster.transform.position - FireHead.position).normalized;
			
			// FireHead.right = (monster.transform.position - FireHead.position).normalized;
			if ( _fireTime < 0 ) {
				var bullet = Instantiate(BulletPrefab);
				bullet.transform.position = FirePoint.position;
				bullet.Init(monster);
				_fireTime = FireInterval;
			}
			_fireTime -= Time.deltaTime;
		}
	}
}