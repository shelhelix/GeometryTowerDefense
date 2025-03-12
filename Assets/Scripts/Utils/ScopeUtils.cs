using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Utils {
    public static class ScopeUtils {
        public static ComponentRegistrationBuilder RegisterComponentFromResources<T>(this IContainerBuilder builder, string path, Lifetime lifetime) where T : Component {
            var component = Resources.Load<T>(path);
            if ( !component ) {
                Debug.LogError($"Can't get component {typeof(T)} from path {path}. Add to VContainer will fail");
            }
            return builder.RegisterComponentInNewPrefab(component, lifetime);
        } 
        
    }
}