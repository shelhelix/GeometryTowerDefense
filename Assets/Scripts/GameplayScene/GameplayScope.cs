using Game.GlobalContext;
using TriInspector;
using VContainer;
using VContainer.Unity;

namespace Game.GameplayScene {
	public class GameplayScope : LifetimeScope {
		[Required] public BattleManager BattleManager;
		
		protected override void Configure(IContainerBuilder builder) {
			builder.Register<SceneLoader>(Lifetime.Scoped);
			builder.RegisterInstance(BattleManager);
			builder.RegisterEntryPoint<GameplayStarter>();
		}
	}
}