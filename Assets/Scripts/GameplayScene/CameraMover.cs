using TriInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameplayScene {
	public class CameraMover : MonoBehaviour, IDragHandler, IEndDragHandler {
		[Required] public Camera Camera;

		Vector3? _lastPosition;
		
		public void OnDrag(PointerEventData eventData) {
			var pos = Camera.ScreenToWorldPoint(eventData.position);
			if ( _lastPosition.HasValue ) {
				var lastPos = Camera.ScreenToWorldPoint(_lastPosition.Value);
				Camera.transform.Translate(lastPos - pos);
			}
			_lastPosition = eventData.position;
		}


		public void OnEndDrag(PointerEventData eventData) {
			_lastPosition = null;
		}
	}
}