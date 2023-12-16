using Bullets;
using UnityEngine;

namespace Enemy
{
    public class EnemyShooter
    {
        private readonly BulletSystem _bulletSystem;
        private readonly BulletConfig _bulletConfig;

        public EnemyShooter(BulletSystem bulletSystem, BulletConfig bulletConfig)
        {
            _bulletSystem = bulletSystem;
            _bulletConfig = bulletConfig;
        }

        public void Shoot(Vector2 position, Vector2 direction)
        {
            _bulletSystem.ShootBullet(new BulletArgs
            {
                IsPlayer = false,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = position,
                Velocity = direction * _bulletConfig.Speed
            });
        }
    }
}
