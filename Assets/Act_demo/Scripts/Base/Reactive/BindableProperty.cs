using System;

namespace Act_demo.Scripts.Base.Reactive
{
  public class BindableProperty<T> where T : IEquatable<T>
  {
    private T _value = default(T);
    public event Action<T> OnValueChanged;

    public T Value
    {
      get => _value;
      set
      {
        if (!_value.Equals(value))
        {
          _value = value;
          OnValueChanged?.Invoke(_value);
        }
      }
    }

    public BindableProperty(T initialValue = default(T))
    {
      _value = initialValue;
    }
  }
}