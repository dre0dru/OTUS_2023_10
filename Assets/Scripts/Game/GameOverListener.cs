using LifecycleEvents;
using UnityEngine;

namespace Game
{
    public sealed class GameOverListener : IFinishListener
    {
        private readonly GameObject _gameOverPanel;

        public GameOverListener(GameObject gameOverPanel)
        {
            _gameOverPanel = gameOverPanel;
        }

        void IFinishListener.OnFinishGame()
        {
            _gameOverPanel.SetActive(true);
        }
    }
}
