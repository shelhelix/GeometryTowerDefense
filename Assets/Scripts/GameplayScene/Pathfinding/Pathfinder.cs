using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	class Cell {
		public readonly Vector3Int Coords;

		public float F => G + H;
		public float G;
		public float H;
		public Cell Parent;

		public Cell(Vector3Int coords, Cell parent) {
			Coords = coords;
			Parent = parent;
		}
	}
	
	public class Pathfinder : MonoBehaviour {
		[Required, SerializeField] BaseMapLayer TowersLayer;
		[Required, SerializeField] BaseMapLayer GroundLayer;

		Grid Grid => GroundLayer.Grid;
		
		Dictionary<Vector3Int, Cell> _cells = new();
		
		public bool CanFindPath(Transform start, Transform end, List<Vector3Int>  additionalWalls = null) {
			return FindPath(start.position, end.position, cell => DefaultIsWalkable(start, end, cell, additionalWalls)) != null;
		}
		
		public List<Vector3> FindPath(Transform start, Transform end) {
			return FindPath(start.position, end.position, cell => DefaultIsWalkable(start, end, cell, null));
		}

		bool DefaultIsWalkable(Transform start, Transform end, Vector3Int cell, List<Vector3Int> additionalTowers) {
			if ( !GroundLayer.HasCell(cell) ) {
				return false;
			}

			var startPos                     = Grid.WorldToCell(start.position);
			var endPos                       = Grid.WorldToCell(end.position);
			var isCellInAdditionalTowersList = additionalTowers?.Contains(cell) ?? false;
			if ( (TowersLayer.HasCell(cell) || isCellInAdditionalTowersList) && (cell != startPos) && (cell != endPos) ) {
				return false;
			}
			return true;
		}

		List<Vector3> FindPath(Vector3 start, Vector3 end, Func<Vector3Int, bool> isWalkable) {
			_cells.Clear();
			var startPos  = Grid.WorldToCell(start);
			var endPos    = Grid.WorldToCell(end);
			var startCell = GetCell(startPos);
			startCell.G = 0;
			startCell.H = GetDistance(startPos, endPos);

			var cellsToSearch = new List<Vector3Int> { startPos };
			while ( cellsToSearch.Count > 0 ) {
				var firstCell = cellsToSearch.First();
				cellsToSearch.RemoveAt(0);
				var currentCell = GetCell(firstCell);
				if ( currentCell.Coords == endPos ) {
					var path = new List<Vector3>();
					while ( currentCell != null ) {
						path.Add(Grid.CellToWorld(currentCell.Coords) + Grid.cellSize / 2);
						currentCell = currentCell.Parent;
					}
					path.Reverse();
					return path;
				}
				var neighbours = new List<Vector3Int> {
					firstCell + Vector3Int.up,
					firstCell + Vector3Int.down,
					firstCell + Vector3Int.left,
					firstCell + Vector3Int.right
				};
				foreach ( var neighbour in neighbours ) {
					if ( !isWalkable(neighbour) ) {
						continue;
					}
					if ( _cells.TryGetValue(neighbour, out var neighbourCell) ) {
						if ( neighbourCell.G > currentCell.G + 1 ) {
							neighbourCell.G = currentCell.G + 1;
							neighbourCell.Parent = currentCell;
						}
					} else {
						var newCell = GetCell(neighbour);
						newCell.G      = currentCell.G + 1;
						newCell.H      = GetDistance(neighbour, endPos);
						newCell.Parent = currentCell;
						cellsToSearch.Add(neighbour);
					}
				}
			}
			return null;
		}

		Cell GetCell(Vector3Int coords) {
			if ( _cells.TryGetValue(coords, out var cell) ) {
				return cell;
			}
			var newCell = new Cell(coords, null);
			_cells[coords] = newCell;
			return newCell;
		}
		
		
		// orthogonal only movements are allowed
		float       GetDistance(Vector3Int start, Vector3Int end) => Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);

	}
}