namespace Presenters.PlayerPanel
{
    public class PlayerPanelPresenter : IPlayerPanelPresenter
    {
        private readonly ICharacterInfoPresenter _characterInfoPresenter;
        private readonly ICharacterLevelPresenter _characterLevelPresenter;
        private readonly ICharacterStatsPresenter _characterStatsPresenter;

        public ICharacterInfoPresenter CharacterInfoPresenter => _characterInfoPresenter;
        public ICharacterLevelPresenter CharacterLevelPresenter => _characterLevelPresenter;
        public ICharacterStatsPresenter CharacterStatsPresenter => _characterStatsPresenter;

        public PlayerPanelPresenter(ICharacterInfoPresenter characterInfoPresenter,
            ICharacterLevelPresenter characterLevelPresenter, ICharacterStatsPresenter characterStatsPresenter)
        {
            _characterInfoPresenter = characterInfoPresenter;
            _characterLevelPresenter = characterLevelPresenter;
            _characterStatsPresenter = characterStatsPresenter;
        }
    }
}
