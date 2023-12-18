using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace PresentationModel.Popups
{
    public class UGUIPopupsController<TPopupBase> : IPopupsController<TPopupBase>
        where TPopupBase : PopupBase
    {
        private readonly UGUIPopupsFactory _popupsFactory;

        private readonly Dictionary<Type, TPopupBase> _openedPopups = new Dictionary<Type, TPopupBase>();

        public UGUIPopupsController(UGUIPopupsFactory popupsFactory)
        {
            _popupsFactory = popupsFactory;
        }

        public TPopup Open<TPopup>(bool skipAnimation)
            where TPopup : TPopupBase
        {
            var popup = GetOrCreateNew<TPopup>();

            Open(popup, skipAnimation);

            return popup;
        }

        private TPopup GetOrCreateNew<TPopup>()
            where TPopup : TPopupBase
        {
            var popup = _popupsFactory.GetOrCreate<TPopup>();
            popup.CloseAction = () =>
            {
                Close(popup, false);
            };

            return popup;
        }

        public void Close<TPopup>(bool skipAnimation)
            where TPopup : TPopupBase
        {
            if (TryGetOpenedPopup<TPopup>(out var popup))
            {
                Close(popup, skipAnimation);
            }
        }

        private void Open(TPopupBase popupBase, bool skipAnimation)
        {
            var type = popupBase.GetType();

            popupBase.InterruptCurrentAnimation();
            popupBase.transform.SetAsLastSibling();

            _openedPopups.Add(type, popupBase);

            popupBase.Open(null, skipAnimation);
        }

        private void Close(TPopupBase popupBase, bool skipAnimation)
        {
            if (popupBase.IsOpened)
            {
                popupBase.InterruptCurrentAnimation();

                _openedPopups.Remove(popupBase.GetType());

                popupBase.Close(() =>
                {
                    if (!popupBase.IsCached)
                    {
                        Object.Destroy(popupBase.gameObject);
                    }
                }, skipAnimation);
            }
        }

        public void CloseAll(bool skipAnimation)
        {
            foreach (var openedPopup in _openedPopups.Values)
            {
                Close(openedPopup, skipAnimation);
            }
        }

        public bool TryGetOpenedPopup<TPopup>(out TPopup popup)
            where TPopup : TPopupBase
        {
            if (_openedPopups.TryGetValue(typeof(TPopup), out var popupBase))
            {
                popup = popupBase as TPopup;
                return true;
            }

            popup = null;
            return false;
        }
    }
}
