using UnityEngine;

namespace Game.GameplayScene {
	public class Bullet : MonoBehaviour {
		public Rigidbody2D Rigidbody;
		public float       Speed;

		public float Lifetime;
		
		void OnTriggerEnter2D(Collider2D other) {
			// Destroy(gameObject);	
		}

		public void Init(Monster monster) {
			Destroy(gameObject, Lifetime);
			Rigidbody.linearVelocity = (monster.transform.position - transform.position).normalized * Speed;
		}
	}
}