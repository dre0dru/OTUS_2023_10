using Atomic.Objects;
using Atomics.Extensions;
using Game.Scripts.Characters;
using UnityEngine;

namespace Game.Scripts.Zombies
{
    public class ZombiesFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private Character _character;

        [SerializeField]
        private Zombie _prefab;

        public Zombie Create(Vector3 position)
        {
            var zombie = Object.Instantiate(_prefab, position, Quaternion.identity, _root);
            zombie.GetVariable<AtomicObjectComponent>(ObjectAPI.Target).Value = _character;

            return zombie;
        }
    }
}
