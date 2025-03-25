	using TriInspector;
	using UnityEngine;
	using VContainer;

	namespace Game.GameplayScene {
	public class MonsterSpawner : MonoBehaviour {
		[Required] public PlayerTower   EndPoint;
		[Required] public Pathfinder    Pathfinder;

		BattleManager _battleManager;
		
		[Inject]
		public void Init(BattleManager battleManager) {
			_battleManager = battleManager;
		} 
		
		public void Spawn(Monster monsterPrefab) {
			Debug.Log("Spawning monster");
			var monster = Instantiate(monsterPrefab);
			var path    = Pathfinder.FindPath(transform, EndPoint.transform);
			monster.Init(_battleManager, path);
		}
		
	}
}