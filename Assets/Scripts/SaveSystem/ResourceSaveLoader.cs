using System.Linq;
using GameEngine;
using Object = UnityEngine.Object;

namespace SaveSystem
{
    public class ResourceSaveLoader : RepositorySaveLoader<ResourcesData, ResourceService>
    {
        public ResourceSaveLoader(ResourceService service, IRepository repository) : base(service, repository)
        {
        }

        protected override ResourcesData ExtractData(ResourceService service)
        {
            return new ResourcesData()
            {
                Resources = service.GetResourcesSnapshotsAsDictionary()
            };
        }

        protected override void RestoreFromData(ResourceService service, ResourcesData data)
        {
            foreach (var resource in service.GetResources())
            {
                if (data.Resources.TryGetValue(resource.ID, out var snapshot))
                {
                    resource.RestoreFromSnapshot(snapshot);
                }
            }
        }
    }
}
