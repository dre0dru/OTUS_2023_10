using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PresentationModel.Popups
{
    public class UGUIPopupsFactory
    {
        private readonly UGUIPopupsPrefabs _popupPrefabs;
        private readonly Transform _root;
        private readonly Dictionary<Type, List<PopupBase>> _popupsCache = new Dictionary<Type, List<PopupBase>>();

        public UGUIPopupsFactory(UGUIPopupsPrefabs popupPrefabs, Transform root)
        {
            _popupPrefabs = popupPrefabs;
            _root = root;
        }

        public TPopup GetOrCreate<TPopup>()
            where TPopup : PopupBase
        {
            var prefab = FindPrefab<TPopup>();

            if (!prefab.IsCached)
            {
                return Create(prefab);
            }

            var cachedPopups = GetOrCreateCache<TPopup>();

            if (!TryGetClosedCached<TPopup>(cachedPopups, out var popup))
            {
                popup = Create(prefab);

                cachedPopups.Add(popup);
            }

            return popup;
        }

        private List<PopupBase> GetOrCreateCache<TPopup>()
            where TPopup : PopupBase
        {
            var type = typeof(TPopup);
            
            if (!_popupsCache.TryGetValue(type, out var cachedPopups))
            {
                cachedPopups = new List<PopupBase>();
                _popupsCache.Add(type, cachedPopups);
            }

            return cachedPopups;
        }

        private bool TryGetClosedCached<TPopup>(List<PopupBase> cachedPopups, out TPopup popup)
            where TPopup : PopupBase
        {
            foreach (var cachedPopup in cachedPopups)
            {
                if (!cachedPopup.IsOpened)
                {
                    popup = cachedPopup as TPopup;
                    return true;
                }
            }

            popup = null;
            return false;
        }

        private TPopup Create<TPopup>(TPopup prefab)
            where TPopup : PopupBase =>
            Object.Instantiate(prefab, _root);

        private TPopup FindPrefab<TPopup>()
            where TPopup : PopupBase =>
            _popupPrefabs.FindPrefab<TPopup>();
    }
}
