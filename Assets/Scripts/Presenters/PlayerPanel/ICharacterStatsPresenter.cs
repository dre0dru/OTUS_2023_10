using System;
using System.Collections.Generic;

namespace Presenters.PlayerPanel
{
    public interface ICharacterStatsPresenter : IDisposable
    {
         event Action<ICharacterStatPresenter> OnStatAdded;
         event Action<ICharacterStatPresenter> OnStatRemoved;

         IEnumerable<ICharacterStatPresenter> StatPresenters { get; }
    }
}
