using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifecycleEvents
{
    public sealed class LifecycleManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private LifecycleManager _lifecycleManager;
        
        private void Awake()
        {
            //как вариант, можно заменить на сериализованное поле с рутами
            var rootGameObjects = GetRootGameObjects();
            AddListenersFromRootGameObjects(rootGameObjects);
        }

        private void AddListenersFromRootGameObjects(GameObject[] rootGameObjects)
        {
            foreach (var rootGameObject in rootGameObjects)
            {
                _lifecycleManager.AddListeners(rootGameObject);
            }
        }
        
        private GameObject[] GetRootGameObjects()
        {
            var scene = SceneManager.GetActiveScene();
            return scene.GetRootGameObjects();
        }
    }
}
