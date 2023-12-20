using Presenters.PlayerPanel;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PlayerPanel
{
    public class CharacterLevelView : MonoBehaviour
    {
        [SerializeField]
        private Text _levelText;

        [SerializeField]
        private Text _levelProgressText;

        [SerializeField]
        private Image _levelProgressBar;

        [SerializeField]
        private Button _levelUpButton;

        [SerializeField]
        private Sprite _canLevelUpBarImage;

        [SerializeField]
        private Sprite _levelledUpBarImage;

        private ICharacterLevelPresenter _characterLevelPresenter;

        private void Awake()
        {
            _levelUpButton.onClick.AddListener(LevelUp);
        }

        private void OnDestroy()
        {
            _levelUpButton.onClick.RemoveListener(LevelUp);
        }

        public void Initialize(ICharacterLevelPresenter presenter)
        {
            _characterLevelPresenter = presenter;

            UpdateLevelText();
            UpdateLevelProgress();

            Subscribe();
        }

        public void ReleasePresenters()
        {
            Unsubscribe();
            _characterLevelPresenter.Dispose();
        }

        private void Unsubscribe()
        {
            _characterLevelPresenter.OnExperienceChanged += UpdateLevelProgress;
            _characterLevelPresenter.OnLevelUp += OnLevelUp;
        }

        private void Subscribe()
        {
            _characterLevelPresenter.OnExperienceChanged += UpdateLevelProgress;
            _characterLevelPresenter.OnLevelUp += OnLevelUp;
        }

        private void OnLevelUp()
        {
            UpdateLevelText();
            UpdateLevelButtonState();
        }

        private void UpdateLevelText()
        {
            _levelText.text = _characterLevelPresenter.CurrentLevelText;
        }

        private void UpdateLevelProgress()
        {
            _levelProgressText.text = _characterLevelPresenter.LevelProgressText;

            _levelProgressBar.fillAmount = _characterLevelPresenter.LevelProgress;
            _levelProgressBar.sprite = _characterLevelPresenter.CanLevelUp ? _canLevelUpBarImage : _levelledUpBarImage;

            UpdateLevelButtonState();
        }

        private void UpdateLevelButtonState()
        {
            _levelUpButton.interactable = _characterLevelPresenter.CanLevelUp;
        }

        private void LevelUp()
        {
            _characterLevelPresenter.LevelUp();
        }
    }
}
