using System;

namespace Common.Unity
{
    public static class NotifiableMonoBehaviourSubscribes
    {
        /// <summary>
        /// подписка на смерть объекта
        /// </summary>
        /// <param name="notifiableMonoBehaviour">объект, куда подписываемся</param>
        /// <param name="onDisposeCallback">колбэк, который вызовется</param>
        /// <returns></returns>
        public static IDisposable SubscribeOnDispose(this NotifiableMonoBehaviour notifiableMonoBehaviour, Action onDisposeCallback)
        {
            if (notifiableMonoBehaviour == null || onDisposeCallback == null)
                return new ActionDisposable(() => { });

            void OnDisposeCallback()
            {
                notifiableMonoBehaviour.OnDisposeEvent -= OnDisposeCallback;
                onDisposeCallback?.Invoke();
            }

            notifiableMonoBehaviour.OnDisposeEvent += OnDisposeCallback;
            return new ActionDisposable(() =>
            {
                notifiableMonoBehaviour.OnDisposeEvent -= OnDisposeCallback;
            });
        }
    }
}