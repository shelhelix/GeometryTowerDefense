using System.Collections.Generic;

namespace Game.Utils {
	public static class RandomUtils {
		public static T GetRandomElementInList<T>(this List<T> values) {
			if ( values.Count == 0 ) {
				return default;
			}
			var randomIndex = UnityEngine.Random.Range(0, values.Count);
			return values[randomIndex];
		}
	}
}