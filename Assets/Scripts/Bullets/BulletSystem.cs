using System.Collections.Generic;
using Level;
using LifecycleEvents;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour, IFixedUpdateListener
    {
        [SerializeField]
        private LevelBounds _levelBounds;

        [SerializeField]
        private BulletSpawner _bulletSpawner;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletsCache = new();

        void IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            CheckBulletsOutOfBounds();
        }
        
        public void ShootBullet(BulletArgs args)
        {
            var bullet = _bulletSpawner.SpawnBullet(args);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            bullet.DealDamage(collision.gameObject);
            RemoveBullet(bullet);
        }

        private void CheckBulletsOutOfBounds()
        {
            _bulletsCache.Clear();
            _bulletsCache.AddRange(_activeBullets);

            for (int i = 0, count = _bulletsCache.Count; i < count; i++)
            {
                var bullet = _bulletsCache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletSpawner.DespawnBullet(bullet);
            }
        }
    }
}
