using UnityEngine;

namespace LifecycleEvents
{
    public static class LifecycleExtensions
    {
        public static void AddListeners(this LifecycleManager lifecycleManager, GameObject root)
        {
            foreach (var lifecycleListener in root.GetComponentsInChildren<ILifecycleListener>())
            {
                lifecycleManager.AddListener(lifecycleListener);
            }
        }

        public static void RemoveListeners(this LifecycleManager lifecycleManager, GameObject root)
        {
            foreach (var lifecycleListener in root.GetComponentsInChildren<ILifecycleListener>())
            {
                lifecycleManager.RemoveListener(lifecycleListener);
            }
        }
    }
}
