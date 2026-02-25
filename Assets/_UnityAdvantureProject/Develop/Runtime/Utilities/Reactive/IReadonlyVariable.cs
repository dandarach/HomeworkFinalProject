using System;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Utilities.Reactive
{
    public interface IReadonlyVariable<T>
    {
        T Value { get; }
        
        IDisposable Subscribe(Action<T, T> action);
    }
}