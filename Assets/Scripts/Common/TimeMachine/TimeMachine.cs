using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class TimeMachineOrder
    {
        public const int Input = -1;
        public const int GameLogic = -1;
        public const int Animation = -1;
    }

    public interface ITimeMachineListener
    {
        public void Update(float dt);
    }
    
    public class TimeMachine : NotifiableMonoBehaviour
    {
        private class ListenerData
        {
            public readonly ITimeMachineListener listener;
            public readonly bool workInPause;

            public ListenerData(ITimeMachineListener listener, bool workInPause)
            {
                this.listener = listener;
                this.workInPause = workInPause;
            }
        }
        
        private readonly SortedList<int, List<ListenerData>> listeners = new();
        private readonly List<ITimeMachineListener> listenersSet = new();
        private bool isPaused;
        private event Action<bool> OnPauseEvent;

        #region singleton
        private static TimeMachine instance;

        public static TimeMachine Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                instance = FindObjectOfType<TimeMachine>();
                if (instance != null)
                    return instance;

                var timeMachineObject = new GameObject("__TimeMachine", typeof(TimeMachine));
                DontDestroyOnLoad(timeMachineObject);
                if (timeMachineObject.TryGetComponent(out instance))
                    return instance;

                throw new Exception("Can't create TimeMachineInstance");
            }
        }
        #endregion

        public bool IsPaused
        {
            get => isPaused;
            set
            {
                if (isPaused != value)
                {
                    isPaused = value;
                    OnPauseEvent?.Invoke(isPaused);
                }
            }
        }

        public IDisposable SubscribeOnPause(Action<bool> callback, bool notifyWhenSubscribe = false)
        {
            if (callback == null)
                return new ActionDisposable(() => { });

            if (notifyWhenSubscribe)
                callback(isPaused);

            OnPauseEvent += callback;
            return new ActionDisposable(() =>
            {
                OnPauseEvent -= callback;
            });
        }
        
        public IDisposable AddListener(int order, ITimeMachineListener listener, bool workInPause = false)
        {
            if (listenersSet.Contains(listener))
            {
                return new ActionDisposable(() => { });
            }

            if (listeners.TryGetValue(order, out var ordererdListeners) != true)
            {
                listeners.Add(order, ordererdListeners = new List<ListenerData>());
            }

            var listenerData = new ListenerData(listener, workInPause);
            ordererdListeners.Add(listenerData);
            listenersSet.Add(listener);
            
            return new ActionDisposable(() =>
            {
                listenersSet.Remove(listener);
                if (listeners.TryGetValue(order, out var ordererdListeners))
                {
                    ordererdListeners.Remove(listenerData);
                }
            });
        }
        
        private void Update()
        {
            var dt = Time.deltaTime;
            foreach (var orderListeners in listeners.Values)
            {
                foreach (var listenerData in orderListeners)
                {
                    if (!IsPaused || listenerData.workInPause)
                    {
                        listenerData.listener.Update(dt);
                    }
                }
            }
        }
    }
}