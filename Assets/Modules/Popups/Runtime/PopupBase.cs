using System;
using UnityEngine;

namespace PresentationModel.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class PopupBase : MonoBehaviour
    {
        public event Action OpenStarted;
        public event Action OpenFinished;
        public event Action CloseStarted;
        public event Action CloseFinished;

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

        protected virtual void OnDestroy()
        {
            ClearEventHandlers();
        }

        internal void Open(Action onComplete, bool skipAnimation)
        {
            SetActive(true);
            SetBlocksRaycasts(false);
            
            IsOpened = true;
            OnOpenStarted();
            OpenStarted?.Invoke();

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
                OnOpenFinished();
                OpenFinished?.Invoke();
                onComplete?.Invoke();
            }
        }

        internal void Close(Action onComplete, bool skipAnimation)
        {
            SetBlocksRaycasts(false);
            
            IsOpened = false;
            OnCloseStarted();
            CloseStarted?.Invoke();

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
                OnCloseFinished();
                CloseFinished?.Invoke();
                onComplete?.Invoke();

                if (IsCached)
                {
                    ClearEventHandlers();
                }
                
                SetActive(false);
            }
        }

        internal void InterruptCurrentAnimation()
        {
            _animation.InterruptCurrentAnimation();
        }

        protected void Close()
        {
            CloseAction?.Invoke();
        }
        
        protected virtual void OnOpenStarted()
        {
            
        }
        
        protected virtual void OnOpenFinished()
        {
            
        }
        
        protected virtual void OnCloseStarted()
        {
            
        }
        
        protected virtual void OnCloseFinished()
        {
            
        }
        
        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void SetBlocksRaycasts(bool isBlocking)
        {
            _canvasGroup.blocksRaycasts = isBlocking;
        }

        private void ClearEventHandlers()
        {
            OpenStarted = null;
            OpenFinished = null;
            CloseStarted = null;
            CloseFinished = null;
        }
    }
}
