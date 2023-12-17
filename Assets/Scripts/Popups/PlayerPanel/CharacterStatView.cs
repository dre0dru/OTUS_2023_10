using System;
using Presenters.PlayerPanel;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PlayerPanel
{
    public class CharacterStatView : MonoBehaviour
    {
        [SerializeField]
        private Text _statText;

        private ICharacterStatPresenter _characterStatPresenter;

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void Initialize(ICharacterStatPresenter presenter)
        {
            Unsubscribe();

            _characterStatPresenter = presenter;

            OnValueChanged(_characterStatPresenter.Value);

            Subscribe();
        }

        private void Unsubscribe()
        {
            if (_characterStatPresenter != null)
            {
                _characterStatPresenter.OnValueChanged -= OnValueChanged;
            }
        }

        private void Subscribe()
        {
            _characterStatPresenter.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(int statValue)
        {
            _statText.text = $"{_characterStatPresenter.Name}: {_characterStatPresenter.Value.ToString()}";
        }
    }
}
