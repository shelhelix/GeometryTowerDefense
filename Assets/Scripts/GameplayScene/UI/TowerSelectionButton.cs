using Game.Utils.UI;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameplayScene {
	public class TowerSelectionButton : MonoBehaviour {
		public            TowerType     TowerType;
		[Required] public TowerPlacer   TowerPlacer;
		[Required] public ButtonWrapper Button;
		[Required] public Image         ButtonBackground;
		[Required] public TMP_Text      ButtonText;

		void Start() {
			Button.RemoveAllAndAddListener(() => TowerPlacer.SelectTower(TowerType));
			ButtonText.text = TowerType.ToString();
		}

		void Update() {
			ButtonBackground.color = TowerPlacer.ActiveTowerType == TowerType ? Color.green : Color.white;
		}
	}
}