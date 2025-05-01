using UnityEngine;

namespace Game.GameplayScene {
	public abstract class BaseMapLayer : MonoBehaviour {
		public abstract Grid Grid { get; }
		
		public abstract bool HasCell(Vector3Int coords);

		public abstract BoundsInt GetBounds();
	}
}