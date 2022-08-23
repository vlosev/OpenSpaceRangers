using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.FSM;
using UnityEngine;

namespace GameSystems
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
        //состояние ожидания
        private class GameTimeMachineWait : FsmState<GameTimeMachine, float>
        {
            private bool continueDay, requiredStopAfterDay;
            private int requiredDaysCount;
            
            public GameTimeMachineWait(GameTimeMachine entity) : base(entity)
            {
            }

            public override void OnEnter()
            {
                entity.OnContinueDays += OnContinueDays;
                entity.inProgress.Value = false;
            }

            public override FsmState<GameTimeMachine, float> Update(float dt)
            {
                if (continueDay)
                    return new GameTimeMachineProgress(entity, requiredDaysCount, requiredStopAfterDay);
                
                return this;
            }

            public override void OnLeave()
            {
                entity.OnContinueDays -= OnContinueDays;
            }
            
            private void OnContinueDays(int days, bool stopAfterDay)
            {
                requiredDaysCount = days;
                requiredStopAfterDay = stopAfterDay;
                continueDay = true;
            }
        }

        //состояние машины, когда день в процессе
        private class GameTimeMachineProgress : FsmState<GameTimeMachine, float>
        {
            private readonly int lastDays;
            private readonly IGameTimeMachineListener[] sortedListeners;

            private bool requiredStop;
            private float elapsedTime;
            
            public GameTimeMachineProgress(GameTimeMachine entity, int days, bool requiredStopAfterDay = false) : base(entity)
            {
                lastDays = days - 1;
                requiredStop = requiredStopAfterDay;
                sortedListeners = entity.orderedListeners.Values.SelectMany(i => i).ToArray();
            }

            public override void OnEnter()
            {
                entity.OnStop += OnStop;
                entity.inProgress.Value = true;
            }

            public override FsmState<GameTimeMachine, float> Update(float dt)
            {
                //считаем линейное время дня и тикаем всех листенеров
                var t = Mathf.Clamp01(elapsedTime / entity.secondsPerDay);
                foreach (var listener in sortedListeners)
                    listener.Update(t);
                
                //проверяем, нужно ли после этого дня остановиться или продолжаем?
                if (elapsedTime >= entity.secondsPerDay)
                {
                    //прибавляем день к текущей дате
                    entity.date.Value = entity.date.Value.AddDays(1);
                    
                    //если было откуда-то требование остановиться, останавливаемся
                    if (requiredStop)
                        return new GameTimeMachineWait(entity);

                    if (lastDays > 0)
                        return new GameTimeMachineProgress(entity, lastDays);
                }

                elapsedTime = Mathf.Clamp(elapsedTime + dt, 0, entity.secondsPerDay);
                return this;
            }

            public override void OnLeave()
            {
                entity.OnStop -= OnStop;
            }

            private void OnStop()
            {
                requiredStop = true;
            }
        }

        // длительность одного дня в секундах, в теории она потом должна где-то настраиваться
        private int secondsPerDay = 3;
        
        // стартовая дата в игровом мире
        private readonly ReactiveProperty<DateTime> date = new(new DateTime(4000, 1, 1));
        private readonly ReactiveProperty<bool> inProgress = new();

        private event Action<int, bool> OnContinueDays;
        private event Action OnStop;

        private Fsm<GameTimeMachine, float> fsm;
        private readonly SortedList<int, List<IGameTimeMachineListener>> orderedListeners = new();
        private readonly List<IGameTimeMachineListener> listenersSet = new();

        public IReadonlyReactiveProperty<bool> InProgress => inProgress;
        public IReadonlyReactiveProperty<DateTime> Date => date;

        protected override void SafeAwake()
        {
            TimeMachine.Instance.AddListener(0, this);

            fsm = new Fsm<GameTimeMachine, float>(new GameTimeMachineWait(this));
        }

        protected override void OnDispose()
        {
            fsm.Dispose();
        }
        
        void ITimeMachineListener.Update(float dt)
        {
            fsm.Update(dt);
        }

        /// <summary>
        /// запускает тайм-машину независимо от срока, но просит остановиться через один день
        /// </summary>
        /// <param name="stopAfterDay">остановиться ли через день?</param>
        public void Go(int days = 1, bool stopAfterDay = false)
        {
            OnContinueDays?.Invoke(days, stopAfterDay);
        }

        public void Stop()
        {
            OnStop?.Invoke();
        }

        public IDisposable AddHandler(int order, IGameTimeMachineListener listener)
        {
            //если у нас в хэше вообще есть этот хэндлер, не добавляем его
            if (listenersSet.Contains(listener))
                return new ActionDisposable(() => { });
            
            //ищем сортированный список и гарантировано кладем туда, если его нет в хэше, значит нет и в списке
            if (orderedListeners.TryGetValue(order, out var ordererdHandlers) != true)
                orderedListeners.Add(order, ordererdHandlers = new List<IGameTimeMachineListener>());

            listenersSet.Add(listener);
            ordererdHandlers.Add(listener);
            return new ActionDisposable(() =>
            {
                if (orderedListeners.TryGetValue(order, out var ordererdHandlers))
                {
                    listenersSet.Remove(listener);
                    ordererdHandlers.Remove(listener);
                }
            });
        }
    }
}