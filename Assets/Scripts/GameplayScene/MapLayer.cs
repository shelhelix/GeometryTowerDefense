using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayScene {
	public class MapLayer : MonoBehaviour {
		public Grid Grid;
		
		Dictionary<Vector3Int, Transform> _cells = new();
		
		void Start() {
			foreach ( Transform child in transform) {
				var gridCoords = Grid.WorldToCell(child.position);
				_cells[gridCoords] = child;
			}
		}
		
		public bool HasCell(Vector3Int coords) => _cells.ContainsKey(coords);
		
		public void AddCell(Transform cell) {
			var coords = Grid.WorldToCell(cell.position);
			_cells[coords] = cell;
			cell.SetParent(transform);
		}
	}
}