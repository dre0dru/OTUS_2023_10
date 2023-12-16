using UnityEngine;

namespace LifecycleEvents
{
    public sealed class LifecycleManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;

        private void Awake()
        {
            //как вариант, можно заменить на сериализованное поле с рутами
            var rootGameObjects = gameObject.scene.GetRootGameObjects();

            foreach (var rootGameObject in rootGameObjects)
            {
                _lifecycleManager.AddListeners(rootGameObject);
            }
        }
    }
}
