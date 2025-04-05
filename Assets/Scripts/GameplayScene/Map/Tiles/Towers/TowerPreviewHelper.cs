using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class TowerPreviewHelper : MonoBehaviour, ITowerPreviewModifier {
		[Required, SerializeField] CircleCollider2D CoverArea;
		[Required, SerializeField] SpriteRenderer   AreaRenderer;
		[Required, SerializeField] Transform        TowerBase;

		[SerializeField] Color Color;
		[SerializeField] Color CannotPlaceColor;

		MaterialPropertyBlock _propertyBlock;

		void Awake() {
			Hide();
		}
		
		[Button]
		public void Show(bool canPlace) {
			_propertyBlock = new();
			_propertyBlock.SetFloat("_Radius", CoverArea.radius * 2);
			_propertyBlock.SetFloat("_MaxRadius", transform.localScale.x);
			_propertyBlock.SetColor("_Color", canPlace ? Color : CannotPlaceColor);
			AreaRenderer.SetPropertyBlock(_propertyBlock);
			gameObject.SetActive(true);
		}
		
		public void SignalCantPlace() {
			DOTween.Kill(TowerBase, true);
			TowerBase.DOShakeRotation(1f, new Vector3(0, 0, 15)).OnComplete(() => TowerBase.rotation = Quaternion.identity);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}
	}
}