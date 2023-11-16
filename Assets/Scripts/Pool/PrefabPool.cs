using UnityEngine;

namespace Pool
{
    public class PrefabPool<T> : UnityObjectPool<T>
        where T : MonoBehaviour
    {
        protected override void OnRelease(T prefab)
        {
            prefab.transform.SetParent(_root);
        }

        protected override void OnCleanup(T prefab)
        {
            Destroy(prefab.gameObject);
        }
    }
}
