using System;
using UnityEngine;

namespace Game.GameplayScene {
	public class Bullet : MonoBehaviour {
		public Rigidbody2D Rigidbody;
		
		public float       Speed;
		public float       Damage;

		public float Lifetime;

		Monster _monster;
		
		void OnTriggerEnter2D(Collider2D other) {
			Destroy(gameObject);
			if ( other.TryGetComponent<Monster>(out var monster) ) {
				monster.TakeDamage(Damage);
			}
		}


		public void Init(Monster monster) {
			Destroy(gameObject, Lifetime);
			_monster = monster;
			UpdateView();
		}

		void Update() {
			if ( !_monster || _monster.IsDying ) {
				return;
			}
			UpdateView();
		}

		void UpdateView() {
			Rigidbody.linearVelocity = (_monster.transform.position - transform.position).normalized * Speed;
			var angle = Mathf.Atan2(Rigidbody.linearVelocityY, Rigidbody.linearVelocityX) * Mathf.Rad2Deg - 90;
			if (angle < 0) {
				angle += 360;
			}
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}