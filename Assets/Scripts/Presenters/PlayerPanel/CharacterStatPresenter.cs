using System;
using Characters;

namespace Presenters.PlayerPanel
{
    public class CharacterStatPresenter : ICharacterStatPresenter, IEquatable<CharacterStatPresenter>
    {
        public event Action<int> OnValueChanged;

        private readonly CharacterStat _characterStat;

        public string Name => _characterStat.Name;
        public int Value => _characterStat.Value;

        public CharacterStatPresenter(CharacterStat characterStat)
        {
            _characterStat = characterStat;

            _characterStat.OnValueChanged += InvokeOnValueChanged;
        }

        ~CharacterStatPresenter()
        {
            _characterStat.OnValueChanged -= InvokeOnValueChanged;
        }

        private void InvokeOnValueChanged(int statValue)
        {
            OnValueChanged?.Invoke(statValue);
        }

        public bool Equals(CharacterStatPresenter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_characterStat, other._characterStat);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CharacterStatPresenter)obj);
        }

        public override int GetHashCode()
        {
            return (_characterStat != null ? _characterStat.GetHashCode() : 0);
        }
    }
}
