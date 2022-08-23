using Common;
using Common.AutoDisposableEvents;
using UnityEngine;

namespace Game
{
    public class GameInput : NotifiableMonoBehaviour, ITimeMachineListener
    {
        private Camera gameCamera;

        /// <summary>
        /// событие главного клика, оно нам необходимо для взаимодействия с миром, кораблем и тд 
        /// </summary>
        public readonly AutoDisposableEvent OnMainClick = new AutoDisposableEvent();
        
        /// <summary>
        /// событие второстепенного клика необходимо для контекстного меню и прочих вещей
        /// </summary>
        public readonly AutoDisposableEvent OnSecondaryClick = new AutoDisposableEvent();
        
        /// <summary>
        /// событие продолжения дня необходимо для запуска игровой тайм-машины
        /// </summary>
        public readonly AutoDisposableEvent OnContinueDay = new AutoDisposableEvent();
        
        protected override void SafeAwake()
        {
            base.SafeAwake();
            gameCamera = Camera.main;

            TimeMachine.Instance.AddListener(TimeMachineOrder.Input, this, true);

            OnMainClick.Subscribe(() =>
            {
                Debug.Log($"get mouse left button down");
            });

            OnSecondaryClick.Subscribe(() =>
            {
                Debug.Log($"get mouse right button down");
            });
        }

        protected override void OnDispose()
        {
        }

        void ITimeMachineListener.Update(float dt)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMainClick.Notify();
                return;
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnSecondaryClick.Notify();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnContinueDay?.Notify();
                return;
            }
        }
    }
}