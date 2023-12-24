using GameEngine;
using SaveSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private Transform _unitsContainer;

        [SerializeField]
        private UnitPrefabs _unitPrefabs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<UnitManager>(Lifetime.Singleton).WithParameter(_unitsContainer);
            builder.Register<ResourceService>(Lifetime.Singleton);

            builder.Register<IRepository<ResourceSaveLoader.Data>, ES3Repository<ResourceSaveLoader.Data>>(Lifetime.Singleton);
            builder.Register<IRepository<UnitSaveLoader.Data>, ES3Repository<UnitSaveLoader.Data>>(Lifetime.Singleton);

            builder.Register<ISaveLoader, ResourceSaveLoader>(Lifetime.Singleton);
            builder.Register<ISaveLoader, UnitSaveLoader>(Lifetime.Singleton).WithParameter(_unitPrefabs);
        }
    }
}
