using System;

namespace Common
{
    public interface IReadonlyReactiveProperty<out T>
    {
        T Value { get; }

        IDisposable SubscribeChanged(Action<T> onChanged, bool notifyWhenSubscribe = false);
    }

    public interface IReactiveProperty<T>
    {
        T Value { get; set; }
        
        IDisposable SubscribeChanged(Action<T> onChanged, bool notifyWhenSubscribe = false);
    }
    
    public class ReactiveProperty<T> :
        IReactiveProperty<T>,
        IReadonlyReactiveProperty<T>
    {
        private T value;
        private Action<T> onChangedCallback;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value) != true)
                {
                    this.value = value;
                    onChangedCallback?.Invoke(value);
                }
            }
        }

        public ReactiveProperty(T value = default)
        {
            this.value = value;
        }

        public IDisposable SubscribeChanged(Action<T> onChanged, bool notifyWhenSubscribe = false)
        {
            if (onChanged == null)
                throw new Exception("can't subscribe null handler");
            
            if (notifyWhenSubscribe)
                onChanged.Invoke(value);

            onChangedCallback += onChanged;
            return new ActionDisposable(() =>
            {
                onChangedCallback -= onChanged;
            });
        }

        public static implicit operator T(ReactiveProperty<T> property)
        {
            return property.value;
        }
    }
}