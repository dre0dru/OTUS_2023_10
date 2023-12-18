using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PresentationModel.Popups
{
    [CreateAssetMenu(fileName = nameof(UGUIPopupsPrefabs), menuName = "Popups/" + nameof(UGUIPopupsPrefabs), order = 0)]
    public class UGUIPopupsPrefabs : ScriptableObject
    {
        [SerializeField]
        protected List<PopupBase> _popupPrefabs;
        
        public TPopup FindPrefab<TPopup>()
            where TPopup : PopupBase =>
            _popupPrefabs.Single(popup => popup.GetType() == typeof(TPopup)) as TPopup;
    }
}
