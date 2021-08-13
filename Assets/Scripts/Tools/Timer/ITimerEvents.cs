using UnityEngine.Events;

namespace Tools.Timer
{
    public interface ITimerEvents
    {
        UnityEvent<float> OnStart { get; }
        UnityEvent OnPause{ get; }
        UnityEvent OnStop { get; }
        UnityEvent<float> OnProgress { get; }
        UnityEvent OnComplete { get; }
    }
}