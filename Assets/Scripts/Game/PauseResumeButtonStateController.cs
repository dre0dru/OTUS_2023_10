using LifecycleEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PauseResumeButtonStateController : MonoBehaviour, IStartListener, IFinishListener, IResumeListener,
        IPauseListener
    {
        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

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
