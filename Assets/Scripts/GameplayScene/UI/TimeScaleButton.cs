using Game.Utils.UI;
using TMPro;
using TriInspector;
using UnityEngine;

namespace Game.GameplayScene {
	public class TimeScaleButton : MonoBehaviour {
		[Required] public ButtonWrapper Button;
		[Required] public TMP_Text      Text;

		public float DesiredTimeScale;

		protected void Awake() {
			Text.text = $"x{DesiredTimeScale}";
			Button.RemoveAllAndAddListener(() => Time.timeScale = DesiredTimeScale);
		}
	}
}