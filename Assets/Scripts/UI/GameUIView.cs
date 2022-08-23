using System;
using Common;
using Patterns.MVVM;
using UI.Viewmodels;
using UI.Windows;
using UnityEngine;

namespace UI
{
    public class GameUiContext
    {
        public readonly IReadonlyReactiveProperty<DateTime> Date;

        public GameUiContext(IReadonlyReactiveProperty<DateTime> date)
        {
            this.Date = date;
        }
    }
    
    public class GameUIView : View<GameUIViewModel>
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private ObjectDescriptionWindow descriptionWindow;
        [SerializeField] private TMPro.TextMeshProUGUI dateText;

        private IDisposable subscribeDate;

        protected override void OnBindViewModel(GameUIViewModel vm)
        {
            if (vm != null)
            {
                subscribeDate = vm.Date.SubscribeChanged(date => { dateText.text = date.ToString("dd MMMM yyyy"); }, true);
            }
        }

        protected override void OnUnbindViewModel(GameUIViewModel vm)
        {
            subscribeDate?.Dispose();
            subscribeDate = null;
        }
        
        public void ShowDescription()
        {
        }
    }
}