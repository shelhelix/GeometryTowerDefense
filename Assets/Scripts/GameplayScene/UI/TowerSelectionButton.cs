using Game.Utils.UI;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.GameplayScene {
	public class TowerSelectionButton : MonoBehaviour {
		[SerializeField]           TowerType     TowerType;
		[Required, SerializeField] ButtonWrapper Button;
		[Required, SerializeField] Image         ButtonBackground;
		[Required, SerializeField] TMP_Text      ButtonText;
		[Required, SerializeField] TMP_Text      PriceText;

		TowerPlacer _towerPlacer;

		[Inject]
		void Init(TowerInfoContainer infoContainer, TowerPlacer towerPlacer) {
			_towerPlacer = towerPlacer;
			var towerInfo = infoContainer.Towers.Find(x => x.TowerType == TowerType);
			Button.RemoveAllAndAddListener(() => _towerPlacer.SelectTower(TowerType));
			ButtonText.text = TowerType.ToString();
			PriceText.text = towerInfo?.Cost.ToString() ?? "N/A";
		}

		void Update() {
			ButtonBackground.color = _towerPlacer.ActiveTowerType == TowerType ? Color.green : Color.white;
		}
	}
}