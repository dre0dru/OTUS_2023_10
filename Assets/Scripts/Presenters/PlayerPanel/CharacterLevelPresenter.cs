using System;
using Characters;

namespace Presenters.PlayerPanel
{
    public class CharacterLevelPresenter : ICharacterLevelPresenter
    {
        public event Action OnExperienceChanged;
        public event Action OnLevelUp;

        private readonly PlayerLevel _playerLevel;

        public string CurrentLevelText => $"Level: {_playerLevel.CurrentLevel.ToString()}";
        public string LevelProgressText =>
            $"XP: {_playerLevel.CurrentExperience.ToString()} / {_playerLevel.RequiredExperience.ToString()}";
        public float LevelProgress => (float)_playerLevel.CurrentExperience / _playerLevel.RequiredExperience;
        public bool CanLevelUp => _playerLevel.CanLevelUp();

        public CharacterLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _playerLevel.OnExperienceChanged += InvokeOnExperienceChanged;
            _playerLevel.OnLevelUp += InvokeOnLevelUp;
        }

        public void Dispose()
        {
            _playerLevel.OnExperienceChanged -= InvokeOnExperienceChanged;
            _playerLevel.OnLevelUp -= InvokeOnLevelUp;
        }

        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }

        private void InvokeOnExperienceChanged(int currentExp)
        {
            OnExperienceChanged?.Invoke();
        }

        private void InvokeOnLevelUp()
        {
            OnLevelUp?.Invoke();
            InvokeOnExperienceChanged(_playerLevel.CurrentExperience);
        }
    }
}
