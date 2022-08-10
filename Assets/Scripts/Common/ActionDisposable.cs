using System;

namespace Common
{
    public class ActionDisposable : IDisposable
    {
        private event Action OnDisposeCallback;

        public ActionDisposable(Action onDisposeCallback)
        {
            this.OnDisposeCallback = onDisposeCallback;
        }
        
        public void Dispose()
        {
            OnDisposeCallback?.Invoke();
            OnDisposeCallback = null;
        }
    }
}