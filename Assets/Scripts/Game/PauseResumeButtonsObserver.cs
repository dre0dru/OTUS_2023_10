using LifecycleEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PauseResumeButtonsObserver : MonoBehaviour
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;

        [SerializeField]
        private Button _pauseButton;

        [SerializeField]
        private Button _resumeButton;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
            _resumeButton.onClick.AddListener(OnResumeClick);
        }

        private void OnDestroy()
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
