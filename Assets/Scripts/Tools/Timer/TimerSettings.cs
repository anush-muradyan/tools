using System;
namespace Tools.Timer
{
    [Serializable]
    public struct TimerSettings
    {
        public float Duration { get; }
        public bool AutoRun { get; }
        public bool DestroyAfterComplete { get; }

        public TimerSettings(float _duration, bool _autoRun = false, bool _destroyAfterComplete = false)
        {
            AutoRun = _autoRun;
            Duration = _duration;
            DestroyAfterComplete = _destroyAfterComplete;
        }
    }
}