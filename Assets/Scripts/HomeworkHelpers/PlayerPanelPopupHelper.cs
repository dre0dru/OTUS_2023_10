using Characters;
using Popups.PlayerPanel;
using Presenters.PlayerPanel;
using Sirenix.OdinInspector;
using UnityEngine;
using CharacterInfo = Characters.CharacterInfo;

namespace HomeworkHelpers
{
    public class PlayerPanelPopupHelper : MonoBehaviour
    {
        [InfoBox("Запустите плеймод, данные персонажей заполнятся, далее используйте кнопке для показа Popup для конкретного персонажа.")]
        [SerializeField]
        private Sprite[] _avatars;

        [SerializeReference, DisableInEditorMode]
        private CharacterDataHelper _utherData;

        [SerializeReference, DisableInEditorMode]
        private CharacterDataHelper _garroshData;

        [SerializeReference, DisableInEditorMode]
        private CharacterDataHelper _jainaData;

        [SerializeField]
        private PlayerPanelPopup _playerPanelPopup;

        private void Awake()
        {
            _utherData = new CharacterDataHelper(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 20),
                    new CharacterStat("Stamina", 25),
                    new CharacterStat("Dexterity", 25),
                    new CharacterStat("Intelligence", 55),
                    new CharacterStat("Damage", 50),
                    new CharacterStat("Regeneration", 5),
                }),
                new PlayerLevel(1, 0),
                new UserInfo("Uther", "I'm the best!", _avatars[0]));

            _garroshData = new CharacterDataHelper(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 20),
                    new CharacterStat("Stamina", 30),
                    new CharacterStat("Dexterity", 30),
                    new CharacterStat("Intelligence", 20),
                    new CharacterStat("Damage", 35),
                    new CharacterStat("Regeneration", 10),
                }),
                new PlayerLevel(2, 100),
                new UserInfo("Garrosh", "I'm the strongest!", _avatars[1]));

            _jainaData = new CharacterDataHelper(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 25),
                    new CharacterStat("Stamina", 15),
                    new CharacterStat("Dexterity", 20),
                    new CharacterStat("Intelligence", 80),
                    new CharacterStat("Damage", 55),
                    new CharacterStat("Regeneration", 2),
                }),
                new PlayerLevel(3, 200),
                new UserInfo("Jaina", "I'm the coolest!", _avatars[2]));
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForUther()
        {
            ShowPopup(_utherData);
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForGarrosh()
        {
            ShowPopup(_garroshData);
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForJaina()
        {
            ShowPopup(_jainaData);
        }

        private void ShowPopup(CharacterDataHelper characterDataHelper)
        {
            var infoPresenter = new CharacterInfoPresenter(characterDataHelper.UserInfo);
            var levelPresenter = new CharacterLevelPresenter(characterDataHelper.PlayerLevel);
            var statsPresenter = new CharacterStatsPresenter(characterDataHelper.CharacterInfo);

            _playerPanelPopup.Initialize(new PlayerPanelPresenter(infoPresenter, levelPresenter, statsPresenter));
            _playerPanelPopup.Show();
        }
    }
}
