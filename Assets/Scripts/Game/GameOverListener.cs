using LifecycleEvents;
using UnityEngine;

namespace Game
{
    public sealed class GameOverListener : MonoBehaviour, IFinishListener
    {
        [SerializeField]
        private GameObject _gameOverPanel;
        
        void IFinishListener.OnFinishGame()
        {
            _gameOverPanel.SetActive(true);
        }
    }
}
