using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameplayScene {
	public class TowerPlacer : MonoBehaviour {
		public MapLayer   GroundLayer;
		public MapLayer   TowerLayer;
		
		public Pathfinder           Pathfinder;
		public List<MonsterSpawner> SpawnPoints;
		public Transform            EndPoint;

		public Tower TowerPrefab;
		Grid Grid => GroundLayer.Grid;
		
		void Update() {
			if ( !Input.GetMouseButtonDown(0) ) {
				return;
			}
			if ( EventSystem.current.IsPointerOverGameObject() ) {
				return;
			}
			
			var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldPosition.z = 0;
			var cellPosition  = Grid.WorldToCell(worldPosition);
			if ( TowerLayer.HasCell(cellPosition) ) {
				Debug.LogError("Can't create tower here. Cell is already used.");
				return;
			}
			if ( !GroundLayer.HasCell(cellPosition) ) {
				Debug.LogError("Can't create tower here. Can't find a ground cell.");
				return;
			}

			foreach ( var spawnPoint in SpawnPoints ) {
				if ( !Pathfinder.CanFindPath(spawnPoint.transform, EndPoint, new List<Vector3Int>{cellPosition}) ) {
					Debug.LogError("Path will be blocked from point {spawnPoint}");
					return;
				}
			}
				
			var adjustedWorld = Grid.CellToWorld(cellPosition);
			adjustedWorld += Grid.cellSize / 2;
			var cell = Instantiate(TowerPrefab, adjustedWorld, Quaternion.identity, GroundLayer.transform);
			cell.Init();
			TowerLayer.AddCell(cell.transform);
		}
	}
}