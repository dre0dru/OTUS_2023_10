using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public abstract class UnityObjectPool<T> : MonoBehaviour
        where T : Object
    {
        [SerializeField]
        private T _prefab;

        [SerializeField]
        protected Transform _root;

        [SerializeField]
        private bool _prewarmOnStart;

        [SerializeField]
        private int _prewarmOnStartCount;

        private ObjectPool<T> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<T>(Create, actionOnRelease: OnRelease, actionOnDestroy: OnCleanup);
        }

        private void Start()
        {
            if (_prewarmOnStart)
            {
                Prewarm(_prewarmOnStartCount);
            }
        }

        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _pool.Release(Create());
            }
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
            return Instantiate(_prefab, _root);
        }

        protected abstract void OnRelease(T prefab);

        protected abstract void OnCleanup(T prefab);
    }
}
