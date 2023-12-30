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
            builder.Register<UnitsFacade>(Lifetime.Singleton).WithParameter(_unitPrefabs);

            builder.Register<ResourceService>(Lifetime.Singleton).WithParameter(GetResourcesInScene());

            builder.Register<IRepository, ES3Repository>(Lifetime.Singleton);

            builder.Register<ISaveLoader, ResourceSaveLoader>(Lifetime.Singleton);
            builder.Register<ISaveLoader, UnitSaveLoader>(Lifetime.Singleton);

            builder.RegisterBuildCallback(SetupDefaultUnits);
        }

        //Конструктор у UnitManager менять нельзя, поэтому сетап дефолтных юнитов со сцены идет отдельно
        //В идеале через конструктор, как сделали у ResourceService, либо как шаг инициализации игры, если бы у нас
        //был некий Loader
        private void SetupDefaultUnits(IObjectResolver container)
        {
            container.Resolve<UnitManager>().SetupUnits(GetUnitsInScene());
        }

        private Resource[] GetResourcesInScene()
        {
            return Object.FindObjectsOfType<Resource>();
        }

        private Unit[] GetUnitsInScene()
        {
            return Object.FindObjectsOfType<Unit>(true);
        }
    }
}
