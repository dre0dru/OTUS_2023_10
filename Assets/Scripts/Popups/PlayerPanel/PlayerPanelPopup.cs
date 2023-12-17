using Presenters.PlayerPanel;
using UnityEngine;

namespace Popups.PlayerPanel
{
    public class PlayerPanelPopup : MonoBehaviour
    {
        [SerializeField]
        private CharacterInfoView _characterInfoView;

        [SerializeField]
        private CharacterLevelView _characterLevelView;

        [SerializeField]
        private CharacterStatsView _characterStatsView;

        public void Initialize(IPlayerPanelPresenter presenter)
        {
            _characterInfoView.Initialize(presenter.CharacterInfoPresenter);
            _characterLevelView.Initialize(presenter.CharacterLevelPresenter);
            _characterStatsView.Initialize(presenter.CharacterStatsPresenter);
        }
    }
}
