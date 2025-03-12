namespace Game.Utils {
	public class PlatformDetector {
		public static bool IsWeb {
			get {
				#if UNITY_WEBGL
				return true;
				#else
				return false;
				#endif
			}
		}
	}
}