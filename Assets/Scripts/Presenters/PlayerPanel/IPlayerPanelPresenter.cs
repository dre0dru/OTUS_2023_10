namespace Presenters.PlayerPanel
{
    public interface IPlayerPanelPresenter
    {
        ICharacterInfoPresenter CharacterInfoPresenter { get; }
        ICharacterLevelPresenter CharacterLevelPresenter { get; }
        ICharacterStatsPresenter CharacterStatsPresenter { get; }
    }
}
