using System;
using Atomics.Extensions;
using Game.Scripts.Characters;
using Game.Scripts.Zombies;
using UnityEngine;

namespace Game.Scripts.UserInterface
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField]
        private Character _character;

        [SerializeField]
        private ZombiesManager _zombiesManager;

        [SerializeField]
        private PlayerStatsView _playerStatsView;

        [SerializeField]
        private EndGameView _endGameView;

        private void Start()
        {
            _playerStatsView.SetPresenter(new PlayerStatsViewPresenter(_character, _zombiesManager));
            _character.GetObservable(ObjectAPI.DeathEvent).Subscribe(ShowEndGame);
        }

        private void ShowEndGame()
        {
            _endGameView.Show();
        }
    }
}
