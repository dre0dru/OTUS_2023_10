using Characters;
using PresentationModel.Popups;
using Presenters.PlayerPanel;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [Header("Popups")]
        [SerializeField]
        private Transform _popupsRoot;

        [SerializeField]
        private UGUIPopupsPrefabs _popupsPrefabs;

        [Header("Characters")]
        [SerializeField]
        private Sprite[] _avatars;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPopupsController<PopupBase>, UGUIPopupsController<PopupBase>>(Lifetime.Singleton);
            builder.Register<UGUIPopupsFactory>(Lifetime.Singleton).WithParameter(_popupsRoot).WithParameter(_popupsPrefabs);

            builder.Register<CharactersData>(Lifetime.Singleton).WithParameter(_avatars);

            builder.RegisterFactory<string, IPlayerPanelPresenter>(container =>
            {
                var charactersData = container.Resolve<CharactersData>();
                return characterName =>
                {
                    var characterData = charactersData.GetCharacterData(characterName);
                    
                    var infoPresenter = new CharacterInfoPresenter(characterData.UserInfo);
                    var levelPresenter = new CharacterLevelPresenter(characterData.PlayerLevel);
                    var statsPresenter = new CharacterStatsPresenter(characterData.CharacterInfo);
                    
                    return new PlayerPanelPresenter(infoPresenter, levelPresenter, statsPresenter);
                };
            }, Lifetime.Singleton);
        }
    }
}
