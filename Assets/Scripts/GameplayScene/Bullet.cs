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
			_monster                 = monster;
			Rigidbody.linearVelocity = (monster.transform.position - transform.position).normalized * Speed;
		}

		void Update() {
			if ( !_monster || _monster.IsDying ) {
				return;
			}
			Rigidbody.linearVelocity = (_monster.transform.position - transform.position).normalized * Speed;
		}
	}
}