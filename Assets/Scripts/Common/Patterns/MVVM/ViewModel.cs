/*
 * основа для удобной реализации паттерна MVVM - Model View ViewModel
 */

using Common;

namespace Patterns.MVVM
{
    public interface IViewModel : IDisposeNotify
    {
    }
    
    public abstract class ViewModel : DisposableObject, IViewModel
    {
    }
}