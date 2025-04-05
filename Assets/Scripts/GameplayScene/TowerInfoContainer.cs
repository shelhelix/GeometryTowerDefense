using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayScene {
	[Serializable]
	public class TowerInfo {
		public TowerType TowerType;
		public int       Cost;
	}
	
	[CreateAssetMenu(fileName = "TowerInfoContainer", menuName = "ScriptableObjects/TowerInfoContainer")]
	public class TowerInfoContainer : ScriptableObject {
		public List<TowerInfo> Towers;
		
		public TowerInfo GetTowerInfo(TowerType towerType) {
			return Towers.Find(x => x.TowerType == towerType);
		}
	}
}