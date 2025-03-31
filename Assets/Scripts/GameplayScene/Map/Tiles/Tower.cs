using DG.Tweening;
using TriInspector;
using UnityEngine;
using UnityEngine.VFX;
using VContainer;

namespace Game.GameplayScene {
	public class Tower : MonoBehaviour {
		[Required] public Transform FireHead;
		[Required] public Transform FirePoint;

		[Required] public TowerTargetingModule TargetingModule;
		
		[Required] public Transform    BaseView;
		[Required] public VisualEffect VisualEffect;
		
		[Required] public Bullet BulletPrefab;
		
		public float FireInterval;
		public float AngularSpeed;

		float _fireTime;

		Sequence _sequence;

		[Inject]
		public void Init() {
			var initialScale = BaseView.localScale;
			_sequence = DOTween.Sequence()
				.Append(BaseView.DOScale(initialScale * 1f, .4f).From(0).SetEase(Ease.OutSine));
			VisualEffect.Play();
		}

		protected void OnDestroy() {
			_sequence?.Kill();
		}
		void Update() {
			var target = TargetingModule.CurrentTarget;
			if ( !target ) {
				return;
			}
			// look at 2d object
			RotateTowerTowardsTarget(target);
			if ( _fireTime < 0 ) {
				var bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
				bullet.Init(target);
				_fireTime = FireInterval;
			}
			_fireTime -= Time.deltaTime;
		}

		void RotateTowerTowardsTarget(Monster target) {
			var direction    = target.transform.position - transform.position;
			var neededAngle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			var currentAngle = FireHead.rotation.eulerAngles.z;
			if (neededAngle < 0) {
				neededAngle += 360;
			}
			var neededShift  = neededAngle - currentAngle;
			var absDif       = Mathf.Abs(neededShift) ;
			var angularShift = Mathf.Sign(neededShift) * Mathf.Min(AngularSpeed * Time.deltaTime, absDif);
			FireHead.rotation = Quaternion.Euler(0, 0, currentAngle + angularShift);
		}
	}
}