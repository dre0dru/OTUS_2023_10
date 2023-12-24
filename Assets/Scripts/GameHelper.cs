    using System;
    using System.Collections.Generic;
    using GameEngine;
    using SaveSystem;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using VContainer;

    public class GameHelper : MonoBehaviour
    {
        [ShowInInspector, DisableInEditorMode]
        private UnitManager _unitManager;

        [ShowInInspector, DisableInEditorMode]
        private ResourceService _resourceService;

        private IEnumerable<ISaveLoader> _saveLoaders;

        [Inject]
        public void Construct(UnitManager unitManager, ResourceService resourceService, IEnumerable<ISaveLoader> saveLoaders)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
            _saveLoaders = saveLoaders;
        }

        private void Start()
        {
            Load();
        }

        [Button, DisableInEditorMode]
        private void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.Save();
            }
        }

        [Button, DisableInEditorMode]
        private void Load()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.Load();
            }
        }
    }
