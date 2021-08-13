using Tools.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Tools
{
    public class TimerExample : MonoBehaviour
    {
        [SerializeField] private Button runButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button destroyButton;
        [SerializeField] private Text durationText;

        private ITimer timer;

        private void OnEnable()
        {
            runButton.onClick.AddListener(onRunButtonClicked);
            pauseButton.onClick.AddListener(onPauseButtonClicked);
            stopButton.onClick.AddListener(onStopButtonClicked);
            destroyButton.onClick.AddListener(onDestroyButtonClicked);
        }

        private void OnDisable()
        {
            runButton.onClick.RemoveListener(onRunButtonClicked);
            pauseButton.onClick.RemoveListener(onPauseButtonClicked);
            stopButton.onClick.RemoveListener(onStopButtonClicked);
            destroyButton.onClick.RemoveListener(onDestroyButtonClicked);
        }

        private void onDestroyButtonClicked()
        {
            if (timer == null)
            {
                return;
            }

            timer.OnStart.RemoveListener(onStart);
            timer.OnProgress.RemoveListener(onProgress);
            timer.OnPause.RemoveListener(onPause);
            timer.OnStop.RemoveListener(onStop);
            timer.OnComplete.RemoveListener(onComplete);
            timer.Dispose();
            timer = null;
        }

        private void onRunButtonClicked()
        {
            createTimer(new TimerSettings(5f));
            timer.Run();
        }

        private void onPauseButtonClicked()
        {
            timer.Pause();
        }

        private void onStopButtonClicked()
        {
            timer.Stop();
        }

        private void createTimer(TimerSettings settings)
        {
            if (timer != null)
            {
                return;
            }

            timer = Timer.CreateTimer(settings);
            timer.OnStart.AddListener(onStart);
            timer.OnProgress.AddListener(onProgress);
            timer.OnPause.AddListener(onPause);
            timer.OnStop.AddListener(onStop);
            timer.OnComplete.AddListener(onComplete);
        }


        private void onStart(float duration)
        {
            durationText.text = duration.ToString("00.0");
        }

        private void onProgress(float delta)
        {
            durationText.text = delta.ToString("00.0");
        }

        private void onPause()
        {
            durationText.text = "Paused";
        }

        private void onStop()
        {
            durationText.text = "NaN";
        }

        private void onComplete()
        {
            durationText.text = "Yey!";
        }
    }
}