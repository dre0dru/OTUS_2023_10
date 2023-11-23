using System.Collections;
using LifecycleEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CountdownGameStarter : MonoBehaviour
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;

        [SerializeField]
        private Text _countdownText;

        [SerializeField]
        private int _countdownSeconds = 3;

        public void StartCountdown()
        {
            StartCoroutine(CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            _countdownText.gameObject.SetActive(true);

            var waitForSecond = new WaitForSeconds(1);

            for (int i = 0; i < _countdownSeconds; i++)
            {
                _countdownText.text = (_countdownSeconds - i).ToString();
                yield return waitForSecond;
            }
            
            _countdownText.gameObject.SetActive(false);
            
            _lifecycleManager.StartGame();
        }
    }
}
