using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameplayScene {
	public class TowerTargetingModule : MonoBehaviour {
		List<Monster> _monsterQueue = new();

		public Monster CurrentTarget {
			get {
				_monsterQueue.RemoveAll(x => !x);
				return _monsterQueue.FirstOrDefault();
			}
		}
		
		void OnTriggerEnter2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsterQueue.Add(monster);
		}

		void OnTriggerExit2D(Collider2D other) {
			var monster = other.GetComponent<Monster>();
			if (!monster) {
				return;
			}
			_monsterQueue.Remove(monster);
		}
	}
}