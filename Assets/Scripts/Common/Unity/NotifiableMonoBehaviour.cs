using System;
using UnityEngine;

namespace Common.Unity
{
    /// <summary>
    /// бызовый класс для многих игровых unity объектов, которым важен порядок вызовов и прочие нотификации
    /// </summary>
    public abstract class NotifiableMonoBehaviour : MonoBehaviour, IDisposable, IDisposeNotify
    {
        private bool isDisposed;
        
        public event Action OnDisposeEvent;
        
        private void Awake()
        {
            SafeAwake();
        }

        private void OnDestroy()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                OnDisposeEvent?.Invoke();
                
                OnDispose();
            }
        }

        protected virtual void SafeAwake()
        {
        }

        protected virtual void OnDispose()
        {
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                OnDisposeEvent?.Invoke();
                
                OnDispose();
            }
            
            Destroy(this);
        }
    }
}