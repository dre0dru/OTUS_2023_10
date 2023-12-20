using System;
using System.Collections.Generic;
using Characters;

namespace Presenters.PlayerPanel
{
    public class CharacterStatsPresenter : ICharacterStatsPresenter
    {
        public event Action<ICharacterStatPresenter> OnStatAdded;
        public event Action<ICharacterStatPresenter> OnStatRemoved;

        private readonly CharacterInfo _characterInfo;
        private readonly Dictionary<CharacterStat, ICharacterStatPresenter> _presenters = new();

        public IEnumerable<ICharacterStatPresenter> StatPresenters => _presenters.Values;

        public CharacterStatsPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            _characterInfo.OnStatAdded += CreatePresenter;
            _characterInfo.OnStatRemoved += RemovePresenter;

            CreatePresenters();
        }

        public void Dispose()
        {
            _characterInfo.OnStatAdded -= CreatePresenter;
            _characterInfo.OnStatRemoved -= RemovePresenter;
        }

        private void CreatePresenters()
        {
            foreach (var characterStat in _characterInfo.GetStats())
            {
                CreatePresenter(characterStat);
            }
        }

        private void CreatePresenter(CharacterStat characterStat)
        {
            var presenter = new CharacterStatPresenter(characterStat);
            _presenters.Add(characterStat, presenter);

            OnStatAdded?.Invoke(presenter);
        }

        private void RemovePresenter(CharacterStat characterStat)
        {
            if (_presenters.Remove(characterStat, out var presenter))
            {
                OnStatRemoved?.Invoke(presenter);
                presenter.Dispose();
            }
        }
    }
}
