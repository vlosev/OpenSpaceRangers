using System;

namespace Common
{
    public static class SubscribeExtensions
    {
        public static IDisposable SubscribeToDispose(this IDisposable disposable, IDisposeNotify disposeNotify)
        {
            if (disposable == null)
                return new ActionDisposable(() => { });

            var disposableAction = new ActionDisposable(disposable.Dispose);
            disposeNotify.OnDisposeEvent += disposableAction.Dispose;
            return new ActionDisposable(() =>
            {
                disposableAction.Dispose();
                disposeNotify.OnDisposeEvent -= disposableAction.Dispose;
            });
        }
        
        public static IDisposable SubscribeToDisable(this IDisposable disposable, IDisableNotify disposeNotify)
        {
            if (disposable == null)
                return new ActionDisposable(() => { });

            var disposableAction = new ActionDisposable(disposable.Dispose);
            disposeNotify.OnDisableEvent += disposableAction.Dispose;
            return new ActionDisposable(() =>
            {
                disposableAction.Dispose();
                disposeNotify.OnDisableEvent -= disposableAction.Dispose;
            });
        }
    }
}