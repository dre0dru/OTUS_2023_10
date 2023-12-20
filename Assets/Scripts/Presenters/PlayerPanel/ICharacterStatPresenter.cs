using System;

namespace Presenters.PlayerPanel
{
    public interface ICharacterStatPresenter : IDisposable
    {
        event Action OnValueChanged;

        string ValueText { get; }
    }
}
