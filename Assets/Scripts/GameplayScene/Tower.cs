using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameplayScene {
	public class Tower : MonoBehaviour {
		public Transform FireHead;
		public Transform FirePoint;
		
		public Bullet BulletPrefab;
		
		public float FireInterval;
		public float AngularSpeed;

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
			_monsters.RemoveAll(x => !x);
			if ( _monsters.Count == 0 ) {
				return;
			}
			var monster = _monsters.First();
			
			// look at 2d object
			var direction    = monster.transform.position - transform.position;
			var neededAngle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			var currentAngle = FireHead.rotation.eulerAngles.z;
			if ( currentAngle > 180 ) {
				currentAngle -= 360;
			}
			Debug.Log($"MOVING: {currentAngle} -> {neededAngle}");
			var neededShift  = neededAngle - currentAngle;
			var absDif       = Mathf.Abs(neededShift) ;
			var angularShift = Mathf.Sign(neededShift) * Mathf.Min(AngularSpeed * Time.deltaTime, absDif);
			Debug.Log("ABS: " + absDif + " MAX ANG SHIFT: " + AngularSpeed * Time.deltaTime + $" Needed shift {neededShift}");
			FireHead.rotation = Quaternion.Euler(0, 0, currentAngle + angularShift);

			if ( _fireTime < 0 ) {
				var bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
				bullet.Init(monster);
				_fireTime = FireInterval;
			}
			_fireTime -= Time.deltaTime;
		}
	}
}