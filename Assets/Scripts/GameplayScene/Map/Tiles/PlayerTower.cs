using TriInspector;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class PlayerTower : MonoBehaviour {
		public int MaxLives;
		
		[ReadOnly] public int CurrentLives;
		
		public void Awake() {
			CurrentLives = MaxLives;
		}

		void OnTriggerEnter2D(Collider2D other) {
			if ( other.TryGetComponent(out Monster monster) ) {
				if ( monster.IsDying ) {
					return;
				}
				monster.Kill();
				CurrentLives--;
			}
		}
	}
}