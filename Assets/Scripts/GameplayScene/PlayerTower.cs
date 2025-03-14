using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class PlayerTower : MonoBehaviour {
		public int MaxLives;
		
		[ReadOnly]
		public int CurrentLives;

		public void Start() {
			CurrentLives = MaxLives;
		}

		void OnTriggerEnter2D(Collider2D other) {
			CurrentLives--;
			if ( other.TryGetComponent(out Monster monster) ) {
				monster.Kill();
			}
		}
	}
}