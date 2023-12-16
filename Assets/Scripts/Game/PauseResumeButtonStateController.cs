using LifecycleEvents;
using UnityEngine.UI;

namespace Game
{
    public sealed class PauseResumeButtonStateController : IStartListener, IFinishListener, IResumeListener,
        IPauseListener
    {
        private readonly Button _pauseButton;
        private readonly Button _resumeButton;

        public PauseResumeButtonStateController((Button pauseButton, Button resumeButton) buttons)
        {
            _pauseButton = buttons.pauseButton;
            _resumeButton = buttons.resumeButton;
        }

        void IStartListener.OnStartGame()
        {
            SetPauseButtonActive(true);
            SetResumeButtonActive(false);
        }

        void IFinishListener.OnFinishGame()
        {
            SetPauseButtonActive(false);
            SetResumeButtonActive(false);
        }

        void IResumeListener.OnResumeGame()
        {
            SetPauseButtonActive(true);
            SetResumeButtonActive(false);
        }

        void IPauseListener.OnPauseGame()
        {
            SetPauseButtonActive(false);
            SetResumeButtonActive(true);
        }

        private void SetPauseButtonActive(bool isActive)
        {
            _pauseButton.gameObject.SetActive(isActive);
        }

        private void SetResumeButtonActive(bool isActive)
        {
            _resumeButton.gameObject.SetActive(isActive);
        }
    }
}
