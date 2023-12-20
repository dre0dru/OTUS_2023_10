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

        public void Initialize(ICharacterInfoPresenter presenter)
        {
            _characterInfoPresenter = presenter;

            UpdateIcon(_characterInfoPresenter.Icon);
            UpdateName(_characterInfoPresenter.Name);
            UpdateDescription(_characterInfoPresenter.Description);

            Subscribe();
        }

        public void ReleasePresenters()
        {
            Unsubscribe();
            _characterInfoPresenter.Dispose();
        }

        private void Unsubscribe()
        {
            _characterInfoPresenter.OnIconChanged -= UpdateIcon;
            _characterInfoPresenter.OnNameChanged -= UpdateName;
            _characterInfoPresenter.OnDescriptionChanged -= UpdateDescription;
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
