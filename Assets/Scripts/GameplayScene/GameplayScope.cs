using Game.GlobalContext;
using TriInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.GameplayScene {
	public class GameplayScope : LifetimeScope {
		[Required, SerializeField] BattleManager      BattleManager;
		[Required, SerializeField] TowerInfoContainer TowerInfoContainer;
		[Required, SerializeField] TowerPlacer        TowerPlacer;
		
		protected override void Configure(IContainerBuilder builder) {
			builder.RegisterInstance(TowerInfoContainer);
			builder.Register<SceneLoader>(Lifetime.Scoped);
			builder.Register<CurrencyManager>(Lifetime.Scoped);
			builder.RegisterInstance(BattleManager);
			builder.RegisterInstance(TowerPlacer);
			builder.RegisterEntryPoint<GameplayStarter>();
		}
	}
}