using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayScene {
	public class MapLayer : MonoBehaviour {
		public Grid Grid;
		
		public Dictionary<Vector3Int, Transform> Cells = new();
		
		public void Init() {
			foreach ( Transform child in transform) {
				var gridCoords = Grid.WorldToCell(child.position);
				Cells[gridCoords] = child;
			}
		}
		
		public bool HasCell(Vector3Int coords) => Cells.ContainsKey(coords);
		
		public void AddCell(Transform cell) {
			var coords = Grid.WorldToCell(cell.position);
			Cells[coords] = cell;
			cell.SetParent(transform);
		}

		public Transform GetCell(Vector3Int position) => Cells.GetValueOrDefault(position);

		public BoundsInt GetBounds() {
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