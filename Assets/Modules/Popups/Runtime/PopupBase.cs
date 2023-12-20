using System;
using UnityEngine;

namespace PresentationModel.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private PopupAnimationBase _animation;

        [Header("Settings")]
        [SerializeField]
        private bool _isCached;

        internal Action CloseAction { private get; set; }

        public bool IsCached => _isCached;

        public bool IsOpened { get; private set; }

        internal void Open(Action onComplete, bool skipAnimation)
        {
            SetActive(true);
            SetBlocksRaycasts(false);
            
            IsOpened = true;

            if (skipAnimation)
            {
                OpenFinishedLocal();
            }
            else
            {
                _animation.PlayOpenAnimation(OpenFinishedLocal);
            }
            
            void OpenFinishedLocal()
            {
                SetBlocksRaycasts(true);
                onComplete?.Invoke();
            }
        }

        internal void Close(Action onComplete, bool skipAnimation)
        {
            SetBlocksRaycasts(false);
            
            IsOpened = false;

            if (skipAnimation)
            {
                CloseFinishedLocal();
            }
            else
            {
                _animation.PlayCloseAnimation(CloseFinishedLocal);
            }

            void CloseFinishedLocal()
            {
                onComplete?.Invoke();
                
                SetActive(false);
            }
        }

        internal void InterruptCurrentAnimation()
        {
            _animation.InterruptCurrentAnimation();
        }

        protected virtual void Close()
        {
            CloseAction?.Invoke();
        }
        
        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void SetBlocksRaycasts(bool isBlocking)
        {
            _canvasGroup.blocksRaycasts = isBlocking;
        }
    }
}
