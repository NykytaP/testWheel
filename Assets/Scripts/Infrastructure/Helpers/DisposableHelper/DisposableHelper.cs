using System;
namespace Infrastructure.Helpers.DisposableHelper
{
    public static class DisposableHelper
    {
        public static IDisposable ToDisposable(this Action action)
        {
            return new DisposableActionWrapper(action);
        }
    }

    public class DisposableActionWrapper : IDisposable
    {
        private Action _cachedAction;
        
        public DisposableActionWrapper(Action action)
        {
            _cachedAction = action;
        }
        
        public void Dispose()
        {
            _cachedAction?.Invoke();
        }
    }
}