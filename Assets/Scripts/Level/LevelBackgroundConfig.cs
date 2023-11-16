using UnityEngine;

namespace Level
{
    [CreateAssetMenu(
        fileName = "LevelBackgroundConfig",
        menuName = "Level/New LevelBackgroundConfig"
    )]
    public class LevelBackgroundConfig : ScriptableObject
    {
        [SerializeField]
        private float _startPositionY;

        [SerializeField]
        private float _endPositionY;

        [SerializeField]
        private float _movingSpeedY;

        public float StartPositionY => _startPositionY;

        public float EndPositionY => _endPositionY;

        public float MovingSpeedY => _movingSpeedY;
    }
}
