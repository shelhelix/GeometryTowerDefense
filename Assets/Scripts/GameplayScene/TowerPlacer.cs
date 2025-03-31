using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.GameplayScene {
	public class TowerPlacer : MonoBehaviour {
		public MapLayer   GroundLayer;
		public MapLayer   TowerLayer;
		
		public Pathfinder           Pathfinder;
		public List<MonsterSpawner> SpawnPoints;
		public Transform            EndPoint;

		public Tower     TowerPrefab;
		public BeanTower BeanTowerPrefab;

		[ReadOnly] public TowerType ActiveTowerType;
		
		Grid             Grid => GroundLayer.Grid;

		LifetimeScope _scope;

		[Inject]
		public void Init(LifetimeScope scope) {
			_scope = scope;
		}

		public void SelectTower(TowerType type) {
			ActiveTowerType = type;
		}
		
		
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
			
			var towerPrefab = GetTowerPrefab(ActiveTowerType);
			var adjustedWorld = Grid.CellToWorld(cellPosition);
			adjustedWorld += Grid.cellSize / 2;
			var cell = Instantiate(towerPrefab, adjustedWorld, Quaternion.identity, GroundLayer.transform);
			_scope.Container.Inject(cell);
			TowerLayer.AddCell(cell.transform);
		}

		GameObject GetTowerPrefab(TowerType towerType) {
			switch ( towerType ) {
				case TowerType.Bullet:
					return TowerPrefab.gameObject;
				case TowerType.Laser:
					return BeanTowerPrefab.gameObject;
				default: {
					Debug.LogError($"Unknown tower type {towerType}. Returning bullet tower");
					return GetTowerPrefab(TowerType.Bullet);
				}
			}
		}
	}
}