using Bullets;
using Components;
using GameInput;
using LifecycleEvents;
using UnityEngine;

namespace Character
{
    public sealed class CharacterShootController : IUpdateListener
    {
        private readonly BulletSystem _bulletSystem;
        private readonly BulletConfig _bulletConfig;
        private readonly InputManager _inputManager;
        private readonly WeaponComponent _weaponComponent;

        public CharacterShootController(BulletSystem bulletSystem, BulletConfig bulletConfig, InputManager inputManager,
            GameObject character)
        {
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;
            _inputManager = inputManager;
            _weaponComponent = character.GetComponent<WeaponComponent>();
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
