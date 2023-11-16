using System;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private bool _isPlayer;
        private int _damage;

        public bool IsPlayer => _isPlayer;

        public int Damage => _damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
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
            _rigidbody.velocity = velocity;
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
