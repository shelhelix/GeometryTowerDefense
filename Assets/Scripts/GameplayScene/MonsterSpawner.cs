	using TriInspector;
	using UnityEngine;

	namespace Game.GameplayScene {
	public class MonsterSpawner : MonoBehaviour {
		[Required] public PlayerTower    EndPoint;
		[Required] public Pathfinder     Pathfinder;

		public void Spawn(Monster monsterPrefab) {
			Debug.Log("Spawning monster");
			var monster = Instantiate(monsterPrefab);
			var path    = Pathfinder.FindPath(transform, EndPoint.transform);
			monster.Init(path);
		}
		
	}
}