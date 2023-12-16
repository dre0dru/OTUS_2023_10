using System;
using UnityEngine.UI;
using VContainer.Unity;

namespace Game
{
    public sealed class StartButtonObserver : IInitializable, IDisposable
    {
        private readonly CountdownGameStarter _countdownGameStarter;
        private readonly Button _startGameButton;

        public StartButtonObserver(CountdownGameStarter countdownGameStarter, Button startGameButton)
        {
            _countdownGameStarter = countdownGameStarter;
            _startGameButton = startGameButton;
        }

        void IInitializable.Initialize()
        {
            _startGameButton.onClick.AddListener(OnStartGameClick);
        }

        void IDisposable.Dispose()
        {
            _startGameButton.onClick.RemoveListener(OnStartGameClick);
        }

        private void OnStartGameClick()
        {
            _startGameButton.gameObject.SetActive(false);
            _countdownGameStarter.StartCountdown();
        }
    }
}
