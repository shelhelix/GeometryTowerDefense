using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

namespace Game.GameplayScene {
	public class Tower : MonoBehaviour {
		public Transform FireHead;
		public Transform FirePoint;
		
		public Transform    BaseView;
		public VisualEffect VisualEffect;
		
		public Bullet BulletPrefab;
		
		public float FireInterval;
		public float AngularSpeed;

		List<Monster> _monsters = new();

		float _fireTime;

		Sequence _sequence;

		public void Init() {
			var initialScale = BaseView.localScale;
			_sequence = DOTween.Sequence()
				.Append(BaseView.DOScale(initialScale * 1f, .4f).From(0).SetEase(Ease.OutSine));
			VisualEffect.Play();
		}

		protected void OnDestroy() {
			_sequence?.Kill();
		}

		void OnTriggerEnter2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsters.Add(monster);
		}

		void OnTriggerExit2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsters.Remove(monster);
		}

		void Update() {
			_monsters.RemoveAll(x => !x);
			if ( _monsters.Count == 0 ) {
				return;
			}
			var monster = _monsters.First();
			
			// look at 2d object
			var direction    = monster.transform.position - transform.position;
			var neededAngle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			var currentAngle = FireHead.rotation.eulerAngles.z;
			if ( currentAngle > 180 ) {
				currentAngle -= 360;
			}
			var neededShift  = neededAngle - currentAngle;
			var absDif       = Mathf.Abs(neededShift) ;
			var angularShift = Mathf.Sign(neededShift) * Mathf.Min(AngularSpeed * Time.deltaTime, absDif);
			FireHead.rotation = Quaternion.Euler(0, 0, currentAngle + angularShift);

			if ( _fireTime < 0 ) {
				var bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
				bullet.Init(monster);
				_fireTime = FireInterval;
			}
			_fireTime -= Time.deltaTime;
		}
	}
}