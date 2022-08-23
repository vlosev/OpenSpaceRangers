using System;
using UnityEngine;

namespace Common
{
    public interface IDisableNotify
    {
        event Action OnDisableEvent;
    }
    
    public interface IDisposeNotify
    {
        event Action OnDisposeEvent;
    }
    
    public abstract class DisposableObject : IDisposeNotify, IDisposable
    {
        public event Action OnDisposeEvent;

        public virtual void Dispose()
        {
            OnDisposeEvent?.Invoke();
        }
    }
    
    public class NotifiableMonoBehaviour : MonoBehaviour, IDisposeNotify, IDisposable
    {
        private bool isDestroyed;
        private bool isDisposed;

        public event Action OnDisposeEvent;

        private void Awake()
        {
            SafeAwake();
        }

        private void OnDestroy()
        {
            DisposeSelf();
        }

        protected virtual void SafeAwake()
        {
        }

        protected virtual void OnDispose()
        {
        }

        public void Dispose()
        {
            if (isDestroyed != true)
            {
                Destroy(gameObject);
            }
        }

        private void DisposeSelf()
        {
            if (isDestroyed || isDisposed)
                return;

            isDestroyed = true;
            isDisposed = true;

            OnDisposeEvent?.Invoke();
            OnDisposeEvent = null;

            OnDispose();
        }
    }
}