using System;
using Characters;
using UnityEngine;

namespace Presenters.PlayerPanel
{
    public class CharacterInfoPresenter : ICharacterInfoPresenter
    {
        public event Action<string> OnNameChanged;
        public event Action<string> OnDescriptionChanged;
        public event Action<Sprite> OnIconChanged;

        private readonly UserInfo _userInfo;

        public string Name => _userInfo.Name;
        public string Description => _userInfo.Description;
        public Sprite Icon => _userInfo.Icon;

        public CharacterInfoPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;

            _userInfo.OnNameChanged += InvokeOnNameChanged;
            _userInfo.OnDescriptionChanged += InvokeOnDescriptionChanged;
            _userInfo.OnIconChanged += InvokeOnIconChanged;
        }

        public void Dispose()
        {
            _userInfo.OnNameChanged -= InvokeOnNameChanged;
            _userInfo.OnDescriptionChanged -= InvokeOnDescriptionChanged;
            _userInfo.OnIconChanged -= InvokeOnIconChanged;
        }

        private void InvokeOnDescriptionChanged(string description)
        {
            OnDescriptionChanged?.Invoke(description);
        }

        private void InvokeOnNameChanged(string name)
        {
            OnNameChanged?.Invoke(name);
        }

        private void InvokeOnIconChanged(Sprite icon)
        {
            OnIconChanged?.Invoke(icon);
        }
    }
}
