using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Bullets
{
    //хотел разделить на BulletSpawner и на BulletOutOfBoundsDestroyer, но вышло не очень, так как
    //близкая связь между уничтожением и возвращением в пул. в общем пусть будет KISS
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private BulletPool _bulletPool;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private LevelBounds _levelBounds;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletsCache = new();

        private void FixedUpdate()
        {
            CheckBulletsOutOfBounds();
        }

        public void ShootBullet(Args args)
        {
            var bullet = GetBullet()
                .SetPosition(args.Position)
                .SetColor(args.Color)
                .SetPhysicsLayer(args.PhysicsLayer)
                .SetDamage(args.Damage)
                .SetIsPlayer(args.IsPlayer)
                .SetVelocity(args.Velocity);

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
                ReleaseBullet(bullet);
            }
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

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}
