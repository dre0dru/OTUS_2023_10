using UnityEngine;

namespace Pool
{
    public class PrefabPool<T> : UnityObjectPool<T>
        where T : MonoBehaviour
    {
        public PrefabPool(T prefab, Transform root) : base(prefab, root)
        {
        }

        protected override void OnRelease(T prefab)
        {
            prefab.transform.SetParent(Root);
        }

        protected override void OnCleanup(T prefab)
        {
            Object.Destroy(prefab.gameObject);
        }
    }
}
