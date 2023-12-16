using System;
using LifecycleEvents;
using UnityEngine.UI;
using VContainer.Unity;

namespace Game
{
    public sealed class PauseResumeButtonsObserver : IInitializable, IDisposable
    {
        private readonly LifecycleManager _lifecycleManager;
        private readonly Button _pauseButton;
        private readonly Button _resumeButton;

        public PauseResumeButtonsObserver(LifecycleManager lifecycleManager,
            (Button pauseButton, Button resumeButton) buttons)
        {
            _lifecycleManager = lifecycleManager;
            _pauseButton = buttons.pauseButton;
            _resumeButton = buttons.resumeButton;
        }

        public void Initialize()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
            _resumeButton.onClick.AddListener(OnResumeClick);
        }

        public void Dispose()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClick);
            _resumeButton.onClick.RemoveListener(OnResumeClick);
        }

        private void OnResumeClick()
        {
            _lifecycleManager.ResumeGame();
        }

        private void OnPauseClick()
        {
            _lifecycleManager.PauseGame();
        }
    }
}
