using System;
using Characters;
using Popups.PlayerPanel;
using PresentationModel.Popups;
using Presenters.PlayerPanel;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace HomeworkHelpers
{
    //фабрика для презентера, тоже забиндить
    public class PlayerPanelPopupHelper : MonoBehaviour
    {
        [InfoBox("Запустите плеймод, данные персонажей заполнятся, далее используйте кнопке для показа Popup для конкретного персонажа.")]
        [SerializeReference, DisableInEditorMode]
        private CharactersData _charactersData; //для модификации модели в рантайме и тестов

        private IPopupsController<PopupBase> _popupsController;
        private Func<string, IPlayerPanelPresenter> _presenterFactory;

        [Inject]
        private void Construct(IPopupsController<PopupBase> popupsController, CharactersData charactersData, Func<string, IPlayerPanelPresenter> factory)
        {
            _popupsController = popupsController;
            _charactersData = charactersData;
            _presenterFactory = factory;
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForUther()
        {
            ShowPopup(CharactersData.Uther);
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForGarrosh()
        {
            ShowPopup(CharactersData.Garrosh);
        }

        [Button(ButtonSizes.Large)]
        private void ShowPopupForJaina()
        {
            ShowPopup(CharactersData.Jaina);
        }

        private void ShowPopup(string characterName)
        {
            if (!_popupsController.TryGetOpenedPopup<PlayerPanelPopup>(out var popup))
            {
                popup = _popupsController.Open<PlayerPanelPopup>(false);
            }

            var presenter = _presenterFactory.Invoke(characterName);

            popup.SetPresenter(presenter);
        }
    }
}
