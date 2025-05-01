using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class MapLayer : BaseMapLayer {
		[Required, SerializeField] Grid GridRefence;
		
		public Dictionary<Vector3Int, Transform> Cells = new();

		public override Grid Grid => GridRefence;
		
		public void Init() {
			foreach ( Transform child in transform) {
				var gridCoords = Grid.WorldToCell(child.position);
				Cells[gridCoords] = child;
			}
		}
		
		public override bool HasCell(Vector3Int coords) => Cells.ContainsKey(coords);
		
		public void AddCell(Transform cell) {
			var coords = Grid.WorldToCell(cell.position);
			Cells[coords] = cell;
			cell.SetParent(transform);
		}

		public Transform GetCell(Vector3Int position) => Cells.GetValueOrDefault(position);

		public override BoundsInt GetBounds() {
			var min = Vector3Int.zero;
			var max = Vector3Int.zero;
			foreach ( var cellInfoPair in Cells ) {
				min = Vector3Int.Min(min, cellInfoPair.Key);
				max = Vector3Int.Max(max, cellInfoPair.Key);
			}
			return new BoundsInt(min, max - min);
		}
	}
}