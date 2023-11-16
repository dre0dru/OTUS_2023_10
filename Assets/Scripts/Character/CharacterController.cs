using Bullets;
using Components;
using GameInput;
using UnityEngine;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private BulletSystem _bulletSystem;

        [SerializeField]
        private BulletConfig _bulletConfig;

        [SerializeField]
        private InputManager _inputManager;

        private void Update()
        {
            Shoot();
            MoveHorizontally();
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
            var weapon = _character.GetComponent<WeaponComponent>();
            _bulletSystem.ShootBullet(new BulletArgs
            {
                IsPlayer = true,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = weapon.Position,
                Velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }

        private void MoveHorizontally()
        {
            _character.GetComponent<MoveComponent>()
                .MoveByRigidbodyVelocity(new Vector2(_inputManager.HorizontalInput, 0) * Time.fixedDeltaTime);
        }
    }
}
