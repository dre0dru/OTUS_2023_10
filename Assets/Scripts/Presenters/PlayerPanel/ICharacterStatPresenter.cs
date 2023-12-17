using System;

namespace Presenters.PlayerPanel
{
    public interface ICharacterStatPresenter
    {
        event Action<int> OnValueChanged;

        string Name { get; }

        int Value { get; }
    }
}
