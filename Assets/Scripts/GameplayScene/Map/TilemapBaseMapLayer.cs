using TriInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameplayScene {
	public class TilemapBaseMapLayer : BaseMapLayer {
		[Required, SerializeField] Tilemap _tilemap;

		public override Grid Grid                       => _tilemap.layoutGrid;
		public override bool HasCell(Vector3Int coords) => _tilemap.HasTile(coords);

		public override BoundsInt GetBounds() => _tilemap.cellBounds;
	}
}