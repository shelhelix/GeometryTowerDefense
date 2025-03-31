using TMPro;
using UnityEngine;
using VContainer;

namespace Game.GameplayScene {
	public class MoneyView : MonoBehaviour {
		public TMP_Text MoneyText;

		CurrencyManager _currencyManager;
		
		[Inject]
		public void Init(CurrencyManager currencyManager) {
			_currencyManager = currencyManager;
		} 
		
		protected void Update() {
			MoneyText.text = $"{_currencyManager.CurrentGold} Gold";
		}
	}
}