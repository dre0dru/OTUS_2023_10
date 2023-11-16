using UnityEngine;

namespace Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private BulletPool _bulletPool;

        [SerializeField]
        private Transform _worldTransform;

        public Bullet SpawnBullet(BulletArgs args)
        {
            return GetBullet()
                .SetPosition(args.Position)
                .SetColor(args.Color)
                .SetPhysicsLayer(args.PhysicsLayer)
                .SetDamage(args.Damage)
                .SetIsPlayer(args.IsPlayer)
                .SetVelocity(args.Velocity);
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
