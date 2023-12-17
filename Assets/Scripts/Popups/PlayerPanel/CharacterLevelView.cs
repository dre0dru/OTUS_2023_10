using System;
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

            Unsubscribe();
        }

        public void Initialize(ICharacterLevelPresenter presenter)
        {
            Unsubscribe();

            _characterLevelPresenter = presenter;

            UpdateLevel();
            UpdateLevelProgress(_characterLevelPresenter.CurrentExperience);

            Subscribe();
        }

        private void Unsubscribe()
        {
            if (_characterLevelPresenter != null)
            {
                _characterLevelPresenter.OnExperienceChanged += UpdateLevelProgress;
                _characterLevelPresenter.OnLevelUp += OnLevelUp;
            }
        }

        private void Subscribe()
        {
            _characterLevelPresenter.OnExperienceChanged += UpdateLevelProgress;
            _characterLevelPresenter.OnLevelUp += OnLevelUp;
        }

        private void OnLevelUp()
        {
            UpdateLevel();
            UpdateLevelButtonState();
        }

        private void UpdateLevel()
        {
            _levelText.text = $"Level: {_characterLevelPresenter.CurrentLevel.ToString()}";
        }

        private void UpdateLevelProgress(int currentExp)
        {
            _levelProgressText.text =
                $"XP: {currentExp.ToString()} / {_characterLevelPresenter.RequiredExperience.ToString()}";

            _levelProgressBar.fillAmount = (float)currentExp / _characterLevelPresenter.RequiredExperience;
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
