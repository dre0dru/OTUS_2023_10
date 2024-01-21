using System;
using Atomics.Extensions;
using Game.Scripts.Characters;
using Game.Scripts.Zombies;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UserInterface
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _health;

        [SerializeField]
        private TextMeshProUGUI _bullets;

        [SerializeField]
        private TextMeshProUGUI _kills;

        private PlayerStatsViewPresenter _presenter;

        public void SetPresenter(PlayerStatsViewPresenter presenter)
        {
            _presenter = presenter;
            _presenter.Refresh += OnRefresh;
            OnRefresh();
        }

        private void OnKillsChanged(string obj)
        {
            _kills.text = obj;
        }

        private void OnAmmoChanged(string obj)
        {
            _bullets.text = obj;
        }

        private void OnHealthChanged(string obj)
        {
            _health.text = obj;
        }

        private void OnRefresh()
        {
            OnKillsChanged(_presenter.Kills);
            OnHealthChanged(_presenter.Health);
            OnAmmoChanged(_presenter.Ammo);
        }
    }

    public class PlayerStatsViewPresenter
    {
        private Character _character;
        private ZombiesManager _zombiesManager;

        public event Action Refresh;

        public string Health => $"Health: {_character.GetValue<int>(ObjectAPI.Health).Value}";
        public string Ammo => $"Bullets: {_character.GetValue<int>(ObjectAPI.AmmoCount).Value}/{_character.GetValue<int>(ObjectAPI.MaxAmmoCount).Value}";
        public string Kills => $"Kills: {_zombiesManager.KilledZombies}";

        public PlayerStatsViewPresenter(Character character, ZombiesManager zombiesManager)
        {
            _character = character;
            character.GetObservable<int>(ObjectAPI.Health).Subscribe(OnHealthChanged);
            character.GetObservable<int>(ObjectAPI.AmmoCount).Subscribe(OnAmmoChanged);
            character.GetObservable<int>(ObjectAPI.MaxAmmoCount).Subscribe(OnAmmoChanged);
            _zombiesManager = zombiesManager;
            _zombiesManager.KilledZombiesCountChanged += OnKillsCountChanged;
        }

        private void OnHealthChanged(int _)
        {
            Refresh?.Invoke();
        }

        private void OnAmmoChanged(int _)
        {
            Refresh?.Invoke();
        }

        private void OnKillsCountChanged()
        {
            Refresh?.Invoke();
        }
    }
}
