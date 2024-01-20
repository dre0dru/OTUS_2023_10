using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Bullets
{
    public class Bullet : AtomicObjectComponent
    {
        [Section]
        [SerializeField]
        private BulletCore _bulletCore;

        private void Awake()
        {
            Compose();
        }

        public override void Compose()
        {
            base.Compose();

            _bulletCore.Compose(this);
        }

        private void OnEnable()
        {
            _bulletCore.OnEnable();
        }

        private void Update()
        {
            _bulletCore.Update();
        }

        private void OnDisable()
        {
            _bulletCore.OnDisable();
        }

        private void OnTriggerEnter(Collider other)
        {
            _bulletCore.OnTriggerEnter(other);
        }
    }
}
