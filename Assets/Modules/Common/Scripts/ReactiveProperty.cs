using System;

namespace Modules.Common
{
    public class ReactiveProperty<T>
    {
        private T _value;

        // событие, вызываемое при изменении значения
        public event Action<T> OnValueChanged;

        public ReactiveProperty(T initialValue = default)
        {
            _value = initialValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
    }
}