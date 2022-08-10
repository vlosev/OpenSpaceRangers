using System;
using System.Collections.Generic;
using Common;
using Common.TimeMachine;
using Common.Unity;

namespace GameTimeMachine
{
    /// <summary>
    /// хэндел игровой логической тайм-машины, которая считает игровую дату, отсчитывает дни и линейно выдает прогресс дня
    /// </summary>
    public interface IGameTimeMachineListener
    {
        public void Update(float dt);
    }
    
    public class GameTimeMachine : NotifiableMonoBehaviour, ITimeMachineListener
    {
        // длительность одного дня в секундах, в теории она потом должна где-то настраиваться
        private int secondsPerDay = 3;
        
        // стартовая дата в игровом мире
        private DateTime dateTime = new DateTime(4000, 1, 1);
        
        private readonly SortedList<int, List<IGameTimeMachineListener>> orderedListeners = new();
        private readonly List<IGameTimeMachineListener> listenersSet = new();

        protected override void SafeAwake()
        {
            TimeMachine.Instance.AddListener(10, this);
        }

        /// <summary>
        /// запускает тайм-машину независимо от срока, но просит остановиться через один день
        /// </summary>
        /// <param name="stopAfterDay">остановиться ли через день?</param>
        public void Go(bool stopAfterDay = false)
        {
        }

        public IDisposable AddHandler(int order, IGameTimeMachineListener listener)
        {
            //если у нас в хэше вообще есть этот хэндлер, не добавляем его
            if (listenersSet.Contains(listener))
                return new ActionDisposable(() => { });
            
            //ищем сортированный список и гарантировано кладем туда, если его нет в хэше, значит нет и в списке
            if (orderedListeners.TryGetValue(order, out var ordererdHandlers) != true)
                orderedListeners.Add(order, ordererdHandlers = new List<IGameTimeMachineListener>());
            
            ordererdHandlers.Add(listener);
            return new ActionDisposable(() =>
            {
                if (orderedListeners.TryGetValue(order, out var ordererdHandlers))
                {
                    ordererdHandlers.Remove(listener);
                }
            });
        }

        void ITimeMachineListener.Update(float dt)
        {
            foreach (var listeners in orderedListeners.Values)
            {
                foreach (var listener in listeners)
                {
                    listener.Update(dt);
                }
            }
        }
    }
}