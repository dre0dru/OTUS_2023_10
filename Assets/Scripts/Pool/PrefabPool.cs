using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public class PrefabPool<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        [SerializeField]
        private T _prefab;

        [SerializeField]
        private Transform _root;

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

        private void OnRelease(T prefab)
        {
            prefab.transform.SetParent(_root);
        }

        private void OnCleanup(T prefab)
        {
            Destroy(prefab.gameObject);
        }
    }
}
