using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private LevelBackgroundConfig _config;

        private float _positionX;

        private float _positionZ;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;

            var position = _transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void FixedUpdate()
        {
            MoveBackground();
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
