using System;
using Presenters.PlayerPanel;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PlayerPanel
{
    public class CharacterInfoView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _descriptionText;

        [SerializeField]
        private Text _nameText;

        private ICharacterInfoPresenter _characterInfoPresenter;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Initialize(ICharacterInfoPresenter presenter)
        {
            Unsubscribe();

            _characterInfoPresenter = presenter;

            UpdateIcon(_characterInfoPresenter.Icon);
            UpdateName(_characterInfoPresenter.Name);
            UpdateDescription(_characterInfoPresenter.Description);

            Subscribe();
        }

        private void Unsubscribe()
        {
            //Не нравится проверка на null. Знаю, что можно в UniRx через CompositeDisposable это решить
            //Но интересно, а как по-хорошему делают без UniRx такие отписки?
            //Или вообще не отписываются? Так как презентер одноразовый и каждый раз новый инстанс создается
            if (_characterInfoPresenter != null)
            {
                _characterInfoPresenter.OnIconChanged -= UpdateIcon;
                _characterInfoPresenter.OnNameChanged -= UpdateName;
                _characterInfoPresenter.OnDescriptionChanged -= UpdateDescription;
            }
        }

        private void Subscribe()
        {
            _characterInfoPresenter.OnIconChanged += UpdateIcon;
            _characterInfoPresenter.OnNameChanged += UpdateName;
            _characterInfoPresenter.OnDescriptionChanged += UpdateDescription;
        }

        private void UpdateIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        private void UpdateDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void UpdateName(string characterName)
        {
            _nameText.text = characterName;
        }
    }
}
