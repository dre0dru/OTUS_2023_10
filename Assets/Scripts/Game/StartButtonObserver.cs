using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StartButtonObserver : MonoBehaviour
    {
        [SerializeField]
        private CountdownGameStarter _countdownGameStarter;
        
        [SerializeField]
        private Button _startGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(OnStartGameClick);
        }

        private void OnDestroy()
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
