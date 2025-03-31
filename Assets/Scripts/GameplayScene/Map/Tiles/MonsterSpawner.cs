	using TriInspector;
	using UnityEngine;
	using VContainer;

	namespace Game.GameplayScene {
	public class MonsterSpawner : MonoBehaviour {
		[Required] public PlayerTower   EndPoint;
		[Required] public Pathfinder    Pathfinder;

		BattleManager   _battleManager;
		CurrencyManager _currencyManager;
		
		[Inject]
		public void Init(BattleManager battleManager, CurrencyManager currencyManager) {
			_battleManager   = battleManager;
			_currencyManager = currencyManager;
		} 
		
		public void Spawn(Monster monsterPrefab) {
			var monster = Instantiate(monsterPrefab);
			var path    = Pathfinder.FindPath(transform, EndPoint.transform);
			monster.Init(_battleManager, _currencyManager, path);
		}
		
	}
}