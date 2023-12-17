using System;
using System.Collections.Generic;

namespace Presenters.PlayerPanel
{
    public interface ICharacterStatsPresenter
    {
         event Action<ICharacterStatPresenter> OnStatAdded;
         event Action<ICharacterStatPresenter> OnStatRemoved;

         IEnumerable<ICharacterStatPresenter> StatPresenters { get; }
    }
}
