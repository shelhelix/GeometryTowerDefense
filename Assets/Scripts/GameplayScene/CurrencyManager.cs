using UnityEngine;

namespace Game.GameplayScene {
	public class CurrencyManager {
		public int CurrentGold { get; private set; }
		
		public void AddGold(int amount) {
			if ( amount < 0 ) {
				Debug.LogWarning($"Something went wrong. Trying to add {amount}");
				return;
			}
			CurrentGold += amount;
		}

		public bool IsEnoughGold(int amount) => CurrentGold >= amount;

		public void SpendGold(int amount) {
			if ( amount < 0 ) {
				Debug.LogWarning($"Something went wrong. Trying to spend {amount} gold");
				return;
			}
			if ( CurrentGold < amount ) {
				Debug.LogWarning($"Not enough gold. Current: {CurrentGold}, trying to spend {amount}");
				return;
			}
			CurrentGold -= amount;
		}
	}
}