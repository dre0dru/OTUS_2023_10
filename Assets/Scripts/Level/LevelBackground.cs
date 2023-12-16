using LifecycleEvents;
using UnityEngine;

namespace Level
{
    //Избавляться от MonoBehaviour не стал, так как не имеет зависимостей и не является зависимостью
    public sealed class LevelBackground : MonoBehaviour, IFixedUpdateListener
    {
        [SerializeField]
        private LevelBackgroundConfig _config;

        private float _positionX;

        private float _positionZ;

        private Transform _transform;

        private void Awake()
        {
            SetupBackground();
        }

        void IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            MoveBackground();
        }

        private void SetupBackground()
        {
            _transform = transform;

            var position = _transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }
        
        private void MoveBackground()
        {
            if (_transform.position.y <= _config.EndPositionY)
            {
                _transform.position = new Vector3(
                    _positionX,
                    _config.StartPositionY,
                    _positionZ
                );
            }

            _transform.position -= new Vector3(
                _positionX,
                _config.MovingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }
    }
}
