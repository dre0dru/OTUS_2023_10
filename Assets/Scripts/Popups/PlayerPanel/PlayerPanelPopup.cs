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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _closeButton.onClick.RemoveListener(Close);
        }

        public void SetPresenter(IPlayerPanelPresenter presenter)
        {
            _characterInfoView.Initialize(presenter.CharacterInfoPresenter);
            _characterLevelView.Initialize(presenter.CharacterLevelPresenter);
            _characterStatsView.Initialize(presenter.CharacterStatsPresenter);
        }
    }
}
