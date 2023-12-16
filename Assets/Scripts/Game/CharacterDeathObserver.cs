using System;
using Components;
using LifecycleEvents;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public sealed class CharacterDeathObserver : IInitializable, IDisposable
    {
        private readonly LifecycleManager _lifecycleManager;
        private readonly HitPointsComponent _characterHitPointsComponent;

        public CharacterDeathObserver(LifecycleManager lifecycleManager, GameObject character)
        {
            _lifecycleManager = lifecycleManager;
            _characterHitPointsComponent = character.GetComponent<HitPointsComponent>();
        }

        public void Initialize()
        {
            _characterHitPointsComponent.OnDeath += OnCharacterDeath;
        }

        public void Dispose()
        {
            _characterHitPointsComponent.OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _lifecycleManager.FinishGame();
        }
    }
}
