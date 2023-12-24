using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using Object = UnityEngine.Object;

namespace SaveSystem
{
    public class ResourceSaveLoader : RepositorySaveLoader<ResourceSaveLoader.Data, ResourceService>
    {
        [Serializable]
        public struct Data
        {
            public Dictionary<string, Resource.Snapshot> Resources;
        }

        public ResourceSaveLoader(ResourceService service, IRepository<Data> repository) : base(service, repository)
        {
        }

        protected override Data ExtractData(ResourceService service)
        {
            return new Data()
            {
                Resources = service.GetResources().Select(resource => resource.GetSnapshot())
                    .ToDictionary(snapshot => snapshot.Id)
            };
        }

        protected override void RestoreFromData(ResourceService service, Data data)
        {
            var resourcesInScene = GetResourcesInScene();

            foreach (var resource in resourcesInScene)
            {
                if (data.Resources.TryGetValue(resource.ID, out var snapshot))
                {
                    resource.RestoreFromSnapshot(snapshot);
                }
            }

            service.SetResources(resourcesInScene);
        }

        protected override void SetupDefaultData(ResourceService service)
        {
            service.SetResources(GetResourcesInScene());
        }

        private Resource[] GetResourcesInScene()
        {
            //ресурсы статичны согласно заданию, соотв. всегда на сцене
            return Object.FindObjectsOfType<Resource>();
        }
    }
}
