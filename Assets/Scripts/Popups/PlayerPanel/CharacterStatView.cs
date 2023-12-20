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

        public void Initialize(ICharacterStatPresenter presenter)
        {
            _characterStatPresenter = presenter;

            UpdateStatValueText();

            Subscribe();
        }

        public void ReleasePresenters()
        {
            Unsubscribe();
            _characterStatPresenter.Dispose();
        }

        private void Unsubscribe()
        {
            _characterStatPresenter.OnValueChanged -= UpdateStatValueText;
        }

        private void Subscribe()
        {
            _characterStatPresenter.OnValueChanged -= UpdateStatValueText;
        }

        private void UpdateStatValueText()
        {
            _statText.text = _characterStatPresenter.ValueText;
        }
    }
}
