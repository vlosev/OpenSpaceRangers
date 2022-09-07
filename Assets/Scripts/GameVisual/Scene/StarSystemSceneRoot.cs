using System;
using System.Collections.Generic;
using Common;
using GameSystems;
using UI;
using UI.Viewmodels;
using UnityEngine;

namespace Game.Visual
{
    public class StarSystemSceneRoot : NotifiableMonoBehaviour, ISceneRootObject
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameUIView gameUIView;
        [SerializeField] private GameInput gameInput;

        private GameTimeMachine gameTimeMachine;

        public void Init(StarSystemSceneRootContext ctx, Action onComplete)
        {
            gameTimeMachine = ctx.GameTimeMachine;
            gameUIView.Bind(new GameUIViewModel(gameTimeMachine.Date));
            
            onComplete?.Invoke();
        }
        
        protected override void SafeAwake()
        {
            gameInput.OnContinueDay.Subscribe(OnContinueDay).SubscribeToDispose(this);
        }

        private void OnContinueDay()
        {
            if (gameTimeMachine.InProgress.Value != true)
            {
                var days = GetDaysForPlaying();
                var stopAfterDay = IsRequireStopAfterDay();

                gameTimeMachine.Go(days, stopAfterDay);
            }
            else
            {
                gameTimeMachine.Stop();
            }
        }

        //сколько нам требуется дней для совершения задуманного?
        private int GetDaysForPlaying()
        {
            return 10;
        }

        //нужно ли остановить игровую механику после завершения текущего дня?
        private bool IsRequireStopAfterDay()
        {
            //TODO: определение того, не слишком ли короткий день или что-то еще, и если нужно остановиться
            return false;
        }
    }
}