using Com.Shelinc.WindowSystem.Modules.WindowSystem;
using Game.GlobalContext;
using GameComponentAttributes.Attributes;
using VContainer;
using VContainer.Unity;

namespace Game.EntryPointScene {
	public class EntryPointScope : LifetimeScope {
		[NotNullReference] public WindowManager WindowManager;
		
		protected override void Configure(IContainerBuilder builder) {
			base.Configure(builder);
			builder.Register<SceneLoader>(Lifetime.Scoped);
			builder.RegisterInstance(WindowManager);
		}
	}
}