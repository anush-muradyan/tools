using System;

namespace Tools.Timer
{
    public interface ITimer : ITimerEvents, IDisposable
    {
        void Run();
        void Stop();
        void Pause();
    }
}