using System.Collections.Generic;
using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class Monster : MonoBehaviour {
		public float Speed;
		public float Health;

		public Collider2D Collider;
		
		[ReadOnly]
		public float CurrentHealth;
		
		bool          _isInited;
		List<Vector3> _currentPath;

		int   _lastPointIndex;
		float _progress;

		Sequence    _sequence;
		
		public bool IsDying { get; private set; }

		public void Init(List<Vector3> path) {
			_isInited          = true;
			_currentPath       = path;
			transform.position = path[0];

			var initialScale = transform.localScale;
			transform.localScale = Vector3.zero;
			_sequence = DOTween.Sequence()
				.Append(transform.DOScale(initialScale * 1.5f, .2f))
				.Append(transform.DOScale(initialScale, .2f));
			CurrentHealth = Health;
		}

		public void TakeDamage(float damage) {
			CurrentHealth -= damage;
			if ( CurrentHealth <= 0 ) {
				Kill();
			}
		}

		void OnDestroy() {
			_sequence?.Kill();
		}

		public void Kill() {
			if ( IsDying ) {
				return;
			}
			IsDying          = true;
			Collider.enabled = false;
			_sequence = DOTween.Sequence()
				.Append(transform.DOScale(Vector3.zero, 0.5f))
				.OnComplete(() => {
					Destroy(gameObject);
				});
		}

		void Update() {
			if ( !_isInited ) {
				return;
			}
			var targetPoint = _currentPath[_lastPointIndex+1];
			if ( IsOnPoint(targetPoint) ) {
				_lastPointIndex++;
				_progress = 0;
			}
			if ( _lastPointIndex == _currentPath.Count-1 ) {
				_isInited = false;
				return;
			}
			targetPoint = _currentPath[_lastPointIndex+1];
			var sourcePoint = _currentPath[_lastPointIndex];
			var traverseTime = GetTraverseTime(sourcePoint, targetPoint);
			_progress += Time.deltaTime / traverseTime;
			transform.position = Vector3.Lerp(sourcePoint, targetPoint, _progress);
		}

		float GetTraverseTime(Vector3 sourcePoint, Vector3 targetPoint) => (targetPoint - sourcePoint).magnitude / Speed;

		bool        IsOnPoint(Vector3 point) => transform.position == point;
	}
}