using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public abstract class UnityObjectPool<T>
        where T : Object
    {
        protected readonly Transform Root;
        private readonly T _prefab;
        private readonly ObjectPool<T> _pool;

        protected UnityObjectPool(T prefab, Transform root)
        {
            _prefab = prefab;
            Root = root;
            _pool = new ObjectPool<T>(Create, actionOnRelease: OnRelease, actionOnDestroy: OnCleanup);
        }

        public T Get()
        {
            return _pool.Get();
        }

        public void Release(T prefab)
        {
            _pool.Release(prefab);
        }

        private T Create()
        {
            return Object.Instantiate(_prefab, Root);
        }

        protected abstract void OnRelease(T prefab);

        protected abstract void OnCleanup(T prefab);
    }
}
