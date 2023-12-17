using System;
using UnityEngine;

namespace Presenters.PlayerPanel
{
    public interface ICharacterInfoPresenter
    {
        event Action<string> OnNameChanged;
        event Action<string> OnDescriptionChanged;
        event Action<Sprite> OnIconChanged;

        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
    }
}
