using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public static class ResourceServiceExtensions
    {
        public static IEnumerable<Resource.Snapshot> GetResourcesSnapshots(this ResourceService service)
        {
            return service.GetResources().Select(resource => resource.GetSnapshot());
        }

        public static Dictionary<string, Resource.Snapshot> GetResourcesSnapshotsAsDictionary(this ResourceService service)
        {
            return service.GetResourcesSnapshots().ToDictionary(snapshot => snapshot.Id);
        }
    }
}
