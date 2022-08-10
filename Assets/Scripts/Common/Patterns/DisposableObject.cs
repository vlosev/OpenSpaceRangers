using System;

namespace Common
{
    public abstract class DisposableObject : IDisposable, IDisposeNotify
    {
        public event Action OnDisposeEvent;
        
        public void Dispose()
        {
            OnDispose();
            OnDisposeEvent?.Invoke();
        }

        public virtual void OnDispose()
        {
        }
    }
}