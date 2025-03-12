using Com.Shelinc.GlobalAudio.Modules.GlobalAudio;
using Com.Shelinc.SceneTransitionEffects.Modules.SceneTransitionEffects;
using Com.Shelinc.SceneTransitionEffects.Modules.SceneTransitionEffects.Implementation;
using Game.GlobalContext.Sound;
using Game.Utils;
using VContainer;
using VContainer.Unity;

namespace Game.GlobalContext {
	// Base scope that is created when the game starts
	public class GlobalScope : LifetimeScope {
		protected override void Configure(IContainerBuilder builder) {
			base.Configure(builder);
			builder.RegisterComponentFromResources<AudioElements>("AudioPlayer", Lifetime.Scoped).DontDestroyOnLoad();
			builder.RegisterComponentFromResources<FadeSceneTransition>(FadeSceneTransition.ResourcePath, Lifetime.Singleton)
				.DontDestroyOnLoad()
				.As<ISceneTransition>();
			builder.Register<SoundSettingsController>(Lifetime.Scoped);
			builder.Register<AudioPlayer>(Lifetime.Scoped);
			builder.Register<BgmManager>(Lifetime.Scoped);
		}
	}
}