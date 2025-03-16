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
			Vector3 direction = monster.transform.position - transform.position;
			float   angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			FireHead.rotation = Quaternion.Euler(0, 0, angle - 90);

			if ( _fireTime < 0 ) {
				var bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
				bullet.Init(monster);
				_fireTime = FireInterval;
			}
			_fireTime -= Time.deltaTime;
		}
	}
}