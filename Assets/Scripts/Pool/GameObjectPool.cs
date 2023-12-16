using UnityEngine;

namespace Pool
{
    public class GameObjectPool : UnityObjectPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, Transform root) : base(prefab, root)
        {
        }

        protected override void OnRelease(GameObject prefab)
        {
            prefab.transform.SetParent(Root);
        }

        protected override void OnCleanup(GameObject prefab)
        {
            Object.Destroy(prefab.gameObject);
        }
    }
}
