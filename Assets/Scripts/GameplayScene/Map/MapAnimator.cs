using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class MapAnimator : MonoBehaviour {
		[Required] public MapLayer BackgroundLayer;
		[Required] public MapLayer TowerLayer;
		
		public async UniTask ShowField() {
			HideLayerCells(BackgroundLayer);
			HideLayerCells(TowerLayer);
			await ShowLayer(BackgroundLayer);
			await ShowLayer(TowerLayer);
		}

		public async UniTask ShowLayer(MapLayer layer) {
			var bounds          = layer.GetBounds();
			var mapCenter       = bounds.center;
			var nearestCell     = layer.Cells.First().Value;
			var nearestDistance = Vector3.Distance(mapCenter, nearestCell.position);
			foreach ( var cellInfo in layer.Cells ) {
				var cellDistance = Vector3.Distance(cellInfo.Value.position, mapCenter);
				if ( cellDistance < nearestDistance ) {
					nearestCell     = cellInfo.Value;
					nearestDistance = cellDistance;
				}
			}
			var cellPos = layer.Grid.WorldToCell(nearestCell.position);
			await PlayAnimation(layer, bounds, cellPos);
		}

		void HideLayerCells(MapLayer layer) {
			foreach ( var tile in layer.Cells.Select(x => x.Value.GetComponent<TileAnimator>()) ) {
				tile.InstantHide();
			}
		}

		async UniTask PlayAnimation(MapLayer layer, BoundsInt bounds, Vector3Int startCell) {
			Debug.Log($"{bounds}");
			var maxDistance = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
			for ( var offset = 0; offset <= maxDistance; offset++ ) {
				var allAnimationTasks = new List<UniTask>();
				for ( var xOffset = -offset; xOffset <= offset; xOffset++ ) {
					var yOffsetJump = (xOffset == -offset) || (xOffset == offset) ? 1 : 2 * offset;
					for (var yOffset = -offset; yOffset <= offset; yOffset += yOffsetJump) {
						var x = startCell.x + xOffset;
						var y = startCell.y + yOffset;
						Debug.Log($"used cell {startCell.x + xOffset} {startCell.y + yOffset}");
						var cell = layer.GetCell(new Vector3Int(x, y, 0));
						if ( !cell ) {
							continue;
						}
						var cellComponent = cell.GetComponent<TileAnimator>();
						if ( !cellComponent ) {
							continue;
						}
						allAnimationTasks.Add(cellComponent.PlayAppearAnimation());
					}
				}
				if ( allAnimationTasks.Count > 0 ) {
					await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
				}
			}
		}
	}
}