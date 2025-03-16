using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayScene {
	public class TowerPlacer : MonoBehaviour {
		public MapLayer   GroundLayer;
		public MapLayer   TowerLayer;
		
		public Pathfinder Pathfinder;
		public Transform  SpawnPoint;
		public Transform  EndPoint;

		public GameObject TowerPrefab;
		Grid Grid => GroundLayer.Grid;
		
		void Update() {
			if ( Input.GetMouseButtonDown(0) ) {
				var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				worldPosition.z = 0;
				var cellPosition  = Grid.WorldToCell(worldPosition);
				if ( TowerLayer.HasCell(cellPosition) ) {
					Debug.LogError("Can't create tower here. Cell is already used.");
					return;
				}
				if ( !Pathfinder.CanFindPath(SpawnPoint, EndPoint, new List<Vector3Int>{cellPosition}) ) {
					Debug.LogError("Path will be blocked");
					return;
				}
				
				var adjustedWorld = Grid.CellToWorld(cellPosition);
				adjustedWorld += Grid.cellSize / 2;
				var cell = Instantiate(TowerPrefab, adjustedWorld, Quaternion.identity, GroundLayer.transform);
				TowerLayer.AddCell(cell.transform);
			}
		}
	}
}