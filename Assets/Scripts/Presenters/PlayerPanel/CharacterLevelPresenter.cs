using System;
using Characters;

namespace Presenters.PlayerPanel
{
    public class CharacterLevelPresenter : ICharacterLevelPresenter
    {
        public event Action<int> OnExperienceChanged;
        public event Action OnLevelUp;

        private readonly PlayerLevel _playerLevel;

        public int CurrentLevel => _playerLevel.CurrentLevel;
        public int CurrentExperience => _playerLevel.CurrentExperience;
        public int RequiredExperience => _playerLevel.RequiredExperience;
        public bool CanLevelUp => _playerLevel.CanLevelUp();

        public CharacterLevelPresenter(PlayerLevel playerLevel)
        {
            _playerLevel = playerLevel;

            _playerLevel.OnExperienceChanged += InvokeOnExperienceChanged;
            _playerLevel.OnLevelUp += InvokeOnLevelUp;
        }

        ~CharacterLevelPresenter()
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
            OnExperienceChanged?.Invoke(currentExp);
        }

        private void InvokeOnLevelUp()
        {
            OnLevelUp?.Invoke();
            InvokeOnExperienceChanged(_playerLevel.CurrentExperience);
        }
    }
}
