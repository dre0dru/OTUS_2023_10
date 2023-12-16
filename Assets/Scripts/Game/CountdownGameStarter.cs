using System;
using Cysharp.Threading.Tasks;
using LifecycleEvents;
using UnityEngine.UI;

namespace Game
{
    public sealed class CountdownGameStarter
    {
        private const int CountdownSeconds = 3;
        
        private readonly LifecycleManager _lifecycleManager;
        private readonly Text _countdownText;

        public CountdownGameStarter(LifecycleManager lifecycleManager, Text countdownText)
        {
            _lifecycleManager = lifecycleManager;
            _countdownText = countdownText;
        }

        public void StartCountdown()
        {
            StartCountdownAsync().Forget();
        }

        private async UniTaskVoid StartCountdownAsync()
        {
            _countdownText.gameObject.SetActive(true);

            const int oneSecond = 1;
            
            for (int i = 0; i < CountdownSeconds; i++)
            {
                _countdownText.text = (CountdownSeconds - i).ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(oneSecond));
            }
            
            _countdownText.gameObject.SetActive(false);
            
            _lifecycleManager.StartGame();
        }
    }
}
