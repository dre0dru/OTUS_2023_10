using System;

namespace Presenters.PlayerPanel
{
    public interface ICharacterLevelPresenter : IDisposable
    {
        event Action OnExperienceChanged;
        event Action OnLevelUp;

        string CurrentLevelText { get; }
        string LevelProgressText { get; }
        float LevelProgress { get; }
        bool CanLevelUp { get; }

        public void LevelUp();
    }
}
