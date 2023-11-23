using LifecycleEvents;
using UnityEngine;

namespace Bullets
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField]
        private BulletPool _bulletPool;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private LifecycleManager _lifecycleManager;

        public Bullet SpawnBullet(BulletArgs args)
        {
            var bullet = GetBullet()
                .SetPosition(args.Position)
                .SetColor(args.Color)
                .SetPhysicsLayer(args.PhysicsLayer)
                .SetDamage(args.Damage)
                .SetIsPlayer(args.IsPlayer)
                .SetVelocity(args.Velocity);
            
            _lifecycleManager.AddListeners(bullet.gameObject);

            return bullet;
        }

        public void DespawnBullet(Bullet bullet)
        {
            _lifecycleManager.RemoveListeners(bullet.gameObject);
            
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
