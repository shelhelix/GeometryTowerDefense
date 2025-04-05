namespace Game.GameplayScene {
	public interface ITowerPreviewModifier {
		void Show(bool canPlace);
		void SignalCantPlace();
		void Hide();
	}
}