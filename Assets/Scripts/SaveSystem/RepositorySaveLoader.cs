using UnityEngine;

namespace SaveSystem
{
    public abstract class RepositorySaveLoader<TData, TService> : ISaveLoader
    {
        private readonly TService _service;
        private readonly IRepository _repository;

        protected RepositorySaveLoader(TService service, IRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public void Save()
        {
            Debug.Log($"Saving data for: {GetType()}");
            _repository.SetData(ExtractData(_service));
        }

        public void Load()
        {
            if (_repository.TryGetData<TData>(out var data))
            {
                Debug.Log($"Loading data for: {GetType()}");
                RestoreFromData(_service, data);
            }
            else
            {
                Debug.Log($"Setting up default data for: {GetType()}");
                SetupDefaultData(_service);
            }
        }

        protected abstract TData ExtractData(TService service);
        protected abstract void RestoreFromData(TService service, TData data);
        protected virtual void SetupDefaultData(TService service){}
    }
}
