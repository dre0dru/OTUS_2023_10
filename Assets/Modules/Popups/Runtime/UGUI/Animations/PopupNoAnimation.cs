using System;
using UnityEngine;

namespace PresentationModel.Popups
{
    public class PopupNoAnimation : PopupAnimationBase
    {
        [SerializeField]
        private GameObject _popupRoot;

        private void Awake()
        {
            if (_popupRoot == null)
            {
                _popupRoot = gameObject;
            }
        }

        public override void PlayOpenAnimation(Action onComplete)
        {
            _popupRoot.SetActive(true);
            onComplete?.Invoke();
        }

        public override void PlayCloseAnimation(Action onComplete)
        {
            _popupRoot.SetActive(false);
            onComplete?.Invoke();
        }

        public override void InterruptCurrentAnimation()
        {
        }
    }
}
