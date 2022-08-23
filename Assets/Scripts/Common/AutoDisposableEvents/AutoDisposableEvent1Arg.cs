using System;

namespace Common.AutoDisposableEvents
{
    public interface IAutoDisposableEvent<T1>
    {
        IDisposable Subscribe(Action<T1> action, bool notifyWhenSubscribe = false);
    }
    
    public class AutoDisposableEvent<T1> : IAutoDisposableEvent<T1>
    {
        private event Action<T1> OnEvent;
        private readonly Func<T1> getValue;

        public AutoDisposableEvent(Func<T1> getValue)
        {
            this.getValue = getValue;
        }

        public IDisposable Subscribe(Action<T1> action, bool notifyWhenSubscribe = false)
        {
            if (action == null)
                return new ActionDisposable(() => { });

            if (notifyWhenSubscribe)
                action(getValue != null ? getValue() : default);

            OnEvent += action;
            return new ActionDisposable(() => OnEvent -= action);
        }

        public void Notify(T1 arg1)
        {
            OnEvent?.Invoke(arg1);
        }
    }
}