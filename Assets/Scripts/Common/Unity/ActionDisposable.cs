using System;

namespace Common
{
    public class ActionDisposable : IDisposable
    {
        private Action action;

        public ActionDisposable(Action action)
        {
            this.action = action;
        }
        
        public void Dispose()
        {
            action?.Invoke();
            action = null;
        }
    }
}