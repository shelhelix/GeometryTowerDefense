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

		TowerType             _currentTowerPreviewType;
		GameObject            _towerPreview;
		ITowerPreviewModifier _towerPreviewModifier;
		
		Grid             Grid => GroundLayer.Grid;

		LifetimeScope      _scope;
		TowerInfoContainer _towerInfoContainer;
		CurrencyManager    _currencyManager;
		
		[Inject]
		public void Init(LifetimeScope scope, TowerInfoContainer towerInfoContainer, CurrencyManager currencyManager) {
			_scope              = scope;
			_towerInfoContainer = towerInfoContainer;
			_currencyManager    = currencyManager;
		}

		public void SelectTower(TowerType type) {
			ActiveTowerType = type;
		}
		
		
		void Update() {
			TryDropSelection();
			UpdatePreview();
			TryPlaceTower();
		}

		void TryDropSelection() {
			if ( Input.GetMouseButtonDown(1) ) {
				SelectTower(TowerType.None);
			}
		}

		void UpdatePreview() {
			if ( ActiveTowerType == TowerType.None ) {
				if ( _towerPreview ) {
					Destroy(_towerPreview);
					_towerPreview            = null;
					_towerPreviewModifier    = null;
					_currentTowerPreviewType = TowerType.None;
				}
				return;
			}
			if ( _currentTowerPreviewType != ActiveTowerType ) {
				Destroy(_towerPreview);
				_towerPreview            = Instantiate(GetTowerPrefab(ActiveTowerType));
				_towerPreviewModifier    = _towerPreview.GetComponentInChildren<ITowerPreviewModifier>(true);
				_currentTowerPreviewType = ActiveTowerType;
			}
			_towerPreviewModifier.Show(CanPlaceTower());
			var worldPosition = GetMouseClampedPosition();
			_towerPreview.transform.position = worldPosition;
		}

		bool CanPlaceTower() {
			var cellPosition = GetMouseCellPosition();
			if ( TowerLayer.HasCell(cellPosition) ) {
				return false;
			}
			if ( !GroundLayer.HasCell(cellPosition) ) {
				return false;
			}
			foreach ( var spawnPoint in SpawnPoints ) {
				if ( Pathfinder.CanFindPath(spawnPoint.transform, EndPoint, new List<Vector3Int> { cellPosition }) ) {
					continue;
				}
				return false;
			}
			var towerInfo = _towerInfoContainer.GetTowerInfo(ActiveTowerType);
			if ( towerInfo == null ) {
				Debug.LogError($"Tower info for {ActiveTowerType} is null");
				return false;
			}
			if ( _currencyManager.CurrentGold < towerInfo.Cost ) {
				return false;
			}
			return true;
		}
		
		void TryPlaceTower() {
			if ( !Input.GetMouseButtonDown(0) ) {
				return;
			}
			if ( ActiveTowerType == TowerType.None ) {
				return;
			}
			if ( EventSystem.current.IsPointerOverGameObject() ) {
				return;
			}
			if ( !CanPlaceTower() ) {
				_towerPreviewModifier.SignalCantPlace();
				return;
			}
			var towerPrefab   = GetTowerPrefab(ActiveTowerType);
			var cell = Instantiate(towerPrefab, GetMouseClampedPosition(), Quaternion.identity, GroundLayer.transform);
			_scope.Container.InjectGameObject(cell);
			TowerLayer.AddCell(cell.transform);
			var towerInfo = _towerInfoContainer.GetTowerInfo(ActiveTowerType);
			_currencyManager.SpendGold(towerInfo.Cost);
		}
		
		Vector3Int GetMouseCellPosition() {
			var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldPosition.z = 0;
			return Grid.WorldToCell(worldPosition);
		}

		Vector3 GetMouseClampedPosition() {
			var cellPosition  = GetMouseCellPosition();
			var adjustedWorld = Grid.CellToWorld(cellPosition);
			return adjustedWorld + Grid.cellSize / 2;
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