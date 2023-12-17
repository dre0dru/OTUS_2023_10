using System;

namespace Presenters.PlayerPanel
{
    public interface ICharacterLevelPresenter
    {
        event Action<int> OnExperienceChanged;
        event Action OnLevelUp;

        int CurrentLevel { get; }
        int CurrentExperience { get; }
        int RequiredExperience { get; }
        bool CanLevelUp { get; }

        public void LevelUp();
    }
}
