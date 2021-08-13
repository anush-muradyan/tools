using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Tools.Timer
{
    public sealed class Timer : MonoBehaviour, ITimer
    {
        public UnityEvent<float> OnStart { get; } = new UnityEvent<float>();
        public UnityEvent OnPause { get; } = new UnityEvent();
        public UnityEvent OnStop { get; } = new UnityEvent();
        public UnityEvent<float> OnProgress { get; } = new UnityEvent<float>();
        public UnityEvent OnComplete { get; } = new UnityEvent();

        private float delta;
        private bool _autoRun;
        private bool disposing;
        private bool pause;
        private bool running;

        private Coroutine coroutine;
        private TimerSettings _timerSettings;
        
        public static ITimer CreateTimer(TimerSettings timerSettings)
        {
            var timerGo = new GameObject("Timer");
            var timer = timerGo.AddComponent<Timer>();
            timer.setup(timerSettings);
            return timer;
        }

        private void setup(TimerSettings timerSettings)
        {
            _timerSettings = timerSettings;
            if (_timerSettings.AutoRun)
            {
                ((ITimer) this).Run();
            }
        }

        

        void ITimer.Run()
        {
            if (running)
            {
                return;
            }

            if (!pause)
            {
                delta = _timerSettings.Duration;
            }
            OnStart?.Invoke(delta);

            coroutine = StartCoroutine(timerRoutine());
            running = true;
            pause = false;
        }

        void ITimer.Stop()
        {
            if (!running && !pause)
            {
                return;
            }

            running = false;
            pause = false;
            StopCoroutine(coroutine);
            OnStop?.Invoke();
        }

        void ITimer.Pause()
        {
            if (pause || !running)
            {
                return;
            }

            pause = true;
            running = false;
            StopCoroutine(coroutine);
            OnPause?.Invoke();
        }

        private IEnumerator timerRoutine()
        {
            while (delta > 0f)
            {
                delta -= Time.deltaTime;
                delta = Mathf.Clamp(delta, 0f, _timerSettings.Duration);
                OnProgress?.Invoke(delta);
                yield return null;
            }

            running = false;
            pause = false;
            OnComplete?.Invoke();
            if (_timerSettings.DestroyAfterComplete)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (disposing)
            {
                return;
            }

            disposing = true;
            ((ITimer) this).Stop();
            Destroy(gameObject);
        }
    }
}