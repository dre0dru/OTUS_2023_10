using System;
using Bullets;
using Components;
using GameInput;
using LifecycleEvents;
using UnityEngine;

namespace Character
{
    public class CharacterShootController : MonoBehaviour, IUpdateListener
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private BulletSystem _bulletSystem;

        [SerializeField]
        private BulletConfig _bulletConfig;

        [SerializeField]
        private InputManager _inputManager;

        private WeaponComponent _weaponComponent;

        private void Awake()
        {
            _weaponComponent = _character.GetComponent<WeaponComponent>();
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            Shoot();
        }

        private void Shoot()
        {
            if (_inputManager.IsShootInputPressed)
            {
                ShootBullet();
            }
        }

        private void ShootBullet()
        {
            _bulletSystem.ShootBullet(new BulletArgs
            {
                IsPlayer = true,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.Position,
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}
