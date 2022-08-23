using System;

namespace Common.AutoDisposableEvents
{
    public interface IAutoDisposableEvent
    {
        IDisposable Subscribe(Action action, bool notifyWhenSubscribe = false);
    }
    
    public class AutoDisposableEvent : IAutoDisposableEvent
    {
        private event Action OnEvent;
        
        public IDisposable Subscribe(Action action, bool notifyWhenSubscribe = false)
        {
            if (action == null)
                return new ActionDisposable(() => { });

            if (notifyWhenSubscribe)
                action();

            OnEvent += action;
            return new ActionDisposable(() => OnEvent -= action);
        }

        public void Notify()
        {
            OnEvent?.Invoke();
        }
    } 
}