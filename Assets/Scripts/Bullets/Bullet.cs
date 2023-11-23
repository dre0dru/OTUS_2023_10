using System;
using LifecycleEvents;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IFixedUpdateListener
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private bool _isPlayer;
        private int _damage;
        private Vector2 _velocity;

        public bool IsPlayer => _isPlayer;

        public int Damage => _damage;

        //пока не стал выносить в отдельный класс, так как другой логики тут нет и негде переиспользовать коллизии
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        void IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            transform.Translate(_velocity * deltaTime);
        }

        public Bullet SetDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public Bullet SetIsPlayer(bool isPlayer)
        {
            _isPlayer = isPlayer;
            return this;
        }

        public Bullet SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
            return this;
        }

        public Bullet SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
            return this;
        }

        public Bullet SetPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }

        public Bullet SetColor(Color color)
        {
            _spriteRenderer.color = color;
            return this;
        }
    }
}
