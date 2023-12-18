using System;
using UnityEngine;

namespace PresentationModel.Popups
{
    public abstract class PopupAnimationBase : MonoBehaviour
    {
        public abstract void PlayOpenAnimation(Action onComplete);
        public abstract void PlayCloseAnimation(Action onComplete);
        public abstract void InterruptCurrentAnimation();
    }
}
