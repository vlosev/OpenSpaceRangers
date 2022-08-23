using System;
using Common;
using Patterns.MVVM;

namespace UI.Viewmodels
{
    public class GameUIViewModel : ViewModel
    {
        public readonly IReadonlyReactiveProperty<DateTime> Date;

        public GameUIViewModel(IReadonlyReactiveProperty<DateTime> date)
        {
            Date = date;
        }
    }
}