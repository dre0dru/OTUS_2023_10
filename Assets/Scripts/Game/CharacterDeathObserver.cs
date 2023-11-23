using Components;
using UnityEngine;

namespace Game
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private GameObject _character;

        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    }
}
