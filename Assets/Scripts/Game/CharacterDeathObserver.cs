using Components;
using LifecycleEvents;
using UnityEngine;

namespace Game
{
    public class CharacterDeathObserver : MonoBehaviour, IStartListener, IFinishListener
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;

        [SerializeField]
        private GameObject _character;

        void IStartListener.OnStartGame()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath += OnCharacterDeath;
        }

        void IFinishListener.OnFinishGame()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _lifecycleManager.FinishGame();
        }
    }
}
