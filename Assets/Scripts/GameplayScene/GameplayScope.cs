using Game.GlobalContext;
using VContainer;
using VContainer.Unity;

namespace Game.GameplayScene {
	public class GameplayScope : LifetimeScope {
		protected override void Configure(IContainerBuilder builder) {
			builder.Register<SceneLoader>(Lifetime.Scoped);
			builder.RegisterEntryPoint<GameplayStarter>();
		}
	}
}