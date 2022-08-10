/*
 * основа для удобной реализации паттерна MVVM - Model View ViewModel
 */

using Common;
using Common.Unity;
using UnityEngine;

namespace Patterns.MVVM
{
    public interface IView<in TViewModel> : IDisposeNotify
        where TViewModel : class, IViewModel
    {
        void Bind(TViewModel viewModel);
    }
    
    public abstract class View<TViewModel> : NotifiableMonoBehaviour,
        IView<TViewModel> where TViewModel : class, IViewModel
    {
        private TViewModel viewModel;

        public TViewModel ViewModel => viewModel;

        public void Bind(TViewModel viewModel)
        {
            void UnbindViewModel()
            {
                this.viewModel.OnDisposeEvent -= UnbindViewModel;
                OnUnbindViewModel(this.viewModel);
            }

            if (this.viewModel != null)
            {
                UnbindViewModel();
                this.viewModel = null;
            }

            this.viewModel = viewModel;
            if (this.viewModel != null)
            {
                this.viewModel.OnDisposeEvent -= UnbindViewModel;
                OnBindViewModel(this.viewModel);
            }
        }

        protected virtual void OnBindViewModel(TViewModel vm)
        {
            Debug.Log($"Bind view model '{vm}' to view '{this}'");
        }

        protected virtual void OnUnbindViewModel(TViewModel vm)
        {
            Debug.Log($"Unbind view model '{vm}' from view '{this}'");
        }
    }
}