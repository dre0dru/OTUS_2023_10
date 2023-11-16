using UnityEngine;

namespace Pool
{
    public class GameObjectPool : UnityObjectPool<GameObject>
    {
        protected override void OnRelease(GameObject prefab)
        {
            prefab.transform.SetParent(_root);
        }

        protected override void OnCleanup(GameObject prefab)
        {
            Destroy(prefab.gameObject);
        }
    }
}
