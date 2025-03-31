using DG.Tweening;
using TriInspector;
using UnityEngine;
using UnityEngine.VFX;
using VContainer;

namespace Game.GameplayScene {
	public class BeanTower : MonoBehaviour {
		[Required] public Transform FireHead;
		[Required] public Transform FirePoint;

		[Required] public TowerTargetingModule TargetingModule;
		
		[Required] public Transform    BaseView;
		[Required] public VisualEffect TowerAppearVfx;

		[Required] public VisualEffect LaserBeamVfx;
		
		public LayerMask LaserBeamCollisionLayerMask;
		
		public float AngularSpeed;
		public float DPS;

		float _fireTime;

		Sequence _sequence;
		
		[Inject]
		public void Init() {
			var initialScale = BaseView.localScale;
			_sequence = DOTween.Sequence()
				.Append(BaseView.DOScale(initialScale * 1f, .4f).From(0).SetEase(Ease.OutSine));
			TowerAppearVfx.Play();
		}

		protected void OnDestroy() {
			_sequence?.Kill();
		}
		void Update() {
			var target = TargetingModule.CurrentTarget;
			if ( !target ) {
				LaserBeamVfx.gameObject.SetActive(false);
				return;
			}
			
			RotateTowerTowardsTarget(target);
			var forward = FireHead.up; 
			Debug.Log(forward);
			var hit      = Physics2D.Raycast(FirePoint.position, forward, float.MaxValue, LaserBeamCollisionLayerMask);
			if ( hit.collider ) {
				Debug.Log(hit.distance);
				Debug.Log(hit.collider.gameObject.name);
				LaserBeamVfx.SetFloat(Shader.PropertyToID("Distance"), hit.distance);
				LaserBeamVfx.gameObject.SetActive(true);
				var monster = hit.collider.GetComponent<Monster>();
				monster.TakeDamage(DPS * Time.deltaTime);
			} else {
				LaserBeamVfx.gameObject.SetActive(false);
			}
		}
		
		

		void RotateTowerTowardsTarget(Monster target) {
			var direction    = target.transform.position - transform.position;
			var neededAngle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			if (neededAngle < 0) {
				neededAngle += 360;
			}
			var currentAngle = FireHead.rotation.eulerAngles.z;
			var neededShift  = neededAngle - currentAngle;
			var absDif       = Mathf.Abs(neededShift) ;
			var angularShift = Mathf.Sign(neededShift) * Mathf.Min(AngularSpeed * Time.deltaTime, absDif);
			FireHead.rotation = Quaternion.Euler(0, 0, currentAngle + angularShift);
		}
	}
}