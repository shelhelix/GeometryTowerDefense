using System;

namespace Com.Shelinc.WindowSystem.Modules.WindowSystem.Core.Implementation {
	public class NoParamsShowRequest<T> : IShowRequest {
		public Type WindowType => typeof(T);
	}
}