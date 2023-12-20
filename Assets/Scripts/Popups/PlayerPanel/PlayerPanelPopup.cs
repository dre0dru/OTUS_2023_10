using PresentationModel.Popups;
using Presenters.PlayerPanel;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PlayerPanel
{
    public class PlayerPanelPopup : PopupBase
    {
        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private CharacterInfoView _characterInfoView;

        [SerializeField]
        private CharacterLevelView _characterLevelView;

        [SerializeField]
        private CharacterStatsView _characterStatsView;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            ReleasePresenters();
        }

        public void SetPresenter(IPlayerPanelPresenter presenter)
        {
            _characterInfoView.Initialize(presenter.CharacterInfoPresenter);
            _characterLevelView.Initialize(presenter.CharacterLevelPresenter);
            _characterStatsView.Initialize(presenter.CharacterStatsPresenter);
        }

        public void ReleasePresenters()
        {
            _characterInfoView.ReleasePresenters();
            _characterLevelView.ReleasePresenters();
            _characterStatsView.ReleasePresenters();
        }

        protected override void Close()
        {
            base.Close();

            ReleasePresenters();
        }
    }
}
