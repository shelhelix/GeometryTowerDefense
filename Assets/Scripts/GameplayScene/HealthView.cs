using TMPro;
using UnityEngine;

namespace Game.GameplayScene {
	public class HealthView : MonoBehaviour {
		public PlayerTower PlayerTower;
		public TMP_Text    Text;

		void Update() {
			OnHealthAmountChanged();
		}

		void OnHealthAmountChanged() {
			Text.text = $"{PlayerTower.CurrentLives}/{PlayerTower.MaxLives} Health";
		}
	}
}