using Common;
using Game;
using GameSystems;
using UI;
using UI.Viewmodels;
using UnityEngine;

public class GameSceneRoot : NotifiableMonoBehaviour
{
    [Header("systems")]
    [SerializeField] private TimeMachine timeMachine;
    [SerializeField] private GameTimeMachine gameTimeMachine;
    [SerializeField] private GameInput gameInput;
    
    [Header("ui")]
    [SerializeField] private GameUIView gameUIView;

    protected override void SafeAwake()
    {
        base.SafeAwake();

        var gameUiVm = new GameUIViewModel(
            gameTimeMachine.Date);

        gameInput.OnContinueDay.Subscribe(OnContinueDay).SubscribeToDispose(this);
        
        gameUIView.Bind(gameUiVm);
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
        return 1;
    }

    //нужно ли остановить игровую механику после завершения текущего дня?
    private bool IsRequireStopAfterDay()
    {
        //TODO: определение того, не слишком ли короткий день или что-то еще, и если нужно остановиться
        return true;
    }
}
