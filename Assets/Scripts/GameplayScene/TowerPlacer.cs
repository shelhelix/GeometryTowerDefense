using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameplayScene {
	public class TowerPlacer : MonoBehaviour {
		public Tilemap    Map;

		public GameObject TowerPrefab;

		List<Vector3Int> _usedCells = new();
		
		void Update() {
			if ( Input.GetMouseButtonDown(0) ) {
				var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				worldPosition.z = 0;
				var cellPosition  = Map.WorldToCell(worldPosition);
				if ( _usedCells.Contains(cellPosition) ) {
					Debug.LogError("Can't create tower here. Cell is already used.");
					return;
				}
				var adjustedWorld = Map.CellToWorld(cellPosition);
				adjustedWorld += Map.cellSize / 2;
				Instantiate(TowerPrefab, adjustedWorld, Quaternion.identity, Map.transform);
				_usedCells.Add(cellPosition);
			}
		}
	}
}