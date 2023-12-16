using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace LifecycleEvents
{
    public enum GameState
    {
        OFF = 0,
        PLAYING = 1,
        PAUSED = 2,
        FINISHED = 3
    }

    public sealed class LifecycleManager : ITickable, ILateTickable, IFixedTickable
    {
        private GameState _state;

        private readonly List<ILifecycleListener> _listeners = new();
        private readonly List<IUpdateListener> _updateListeners = new();
        private readonly List<IFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateListener> _lateUpdateListeners = new();

        public LifecycleManager(IEnumerable<ILifecycleListener> lifecycleListeners)
        {
            foreach (var listener in lifecycleListeners)
            {
                AddListener(listener);
            }
        }
        
        void ITickable.Tick()
        {
            if (_state != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = _updateListeners.Count - 1; i >= 0; i--)
            {
                var listener = _updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        void IFixedTickable.FixedTick()
        {
            if (_state != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.fixedDeltaTime;
            for (int i = _fixedUpdateListeners.Count - 1; i >= 0; i--)
            {
                var listener = _fixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        void ILateTickable.LateTick()
        {
            if (_state != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = _lateUpdateListeners.Count - 1; i >= 0; i--)
            {
                var listener = _lateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }

        public void AddListener(ILifecycleListener listener)
        {
            if (listener == null)
            {
                return;
            }

            _listeners.Add(listener);

            if (listener is IUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }

            if (listener is IFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is ILateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(ILifecycleListener listener)
        {
            if (listener == null)
            {
                return;
            }

            _listeners.Remove(listener);

            if (listener is IUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }

            if (listener is IFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is ILateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        public void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            _state = GameState.PLAYING;
        }

        public void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IPauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }

            _state = GameState.PAUSED;
        }

        public void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }

            _state = GameState.PLAYING;
        }

        public void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }

            _state = GameState.FINISHED;
        }
    }
}
