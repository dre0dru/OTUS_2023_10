using Pool;
using UnityEngine;

namespace Bullets
{
    public class BulletSpawner
    {
        private readonly PrefabPool<Bullet> _bulletPool;
        private readonly Transform _worldTransform;

        public BulletSpawner(PrefabPool<Bullet> bulletPool, Transform worldTransform)
        {
            _bulletPool = bulletPool;
            _worldTransform = worldTransform;
        }

        public Bullet SpawnBullet(BulletArgs args)
        {
            var bullet = GetBullet()
                .SetPosition(args.Position)
                .SetColor(args.Color)
                .SetPhysicsLayer(args.PhysicsLayer)
                .SetDamage(args.Damage)
                .SetIsPlayer(args.IsPlayer)
                .SetVelocity(args.Velocity);

            return bullet;
        }

        public void DespawnBullet(Bullet bullet)
        {
            ReleaseBullet(bullet);
        }

        private Bullet GetBullet()
        {
            var bullet = _bulletPool.Get();
            bullet.transform.SetParent(_worldTransform);

            return bullet;
        }

        private void ReleaseBullet(Bullet bullet)
        {
            _bulletPool.Release(bullet);
        }
    }
}
