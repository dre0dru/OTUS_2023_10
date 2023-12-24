using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour
    {
        public struct Snapshot
        {
            public string Type;
            public int HitPoints;
            public Vector3 Position;
            public Vector3 Rotation;
        }
        
        public string Type
        {
            get => type;
        }

        public int HitPoints
        {
            get => hitPoints;
            set => hitPoints = value;
        }

        public Vector3 Position
        {
            get => this.transform.position;
        }
        
        public Vector3 Rotation
        {
            get => this.transform.eulerAngles;
        }

        [SerializeField]
        private string type;
        
        [SerializeField]
        private int hitPoints;

        private void Reset()
        {
            this.type = this.name;
            this.hitPoints = 10;
        }

        public Snapshot GetSnapshot()
        {
            return new Snapshot()
            {
                Type = type,
                HitPoints = hitPoints,
                Position = transform.position,
                Rotation = transform.eulerAngles
            };
        }

        public void RestoreFromSnapshot(Snapshot snapshot)
        {
            type = snapshot.Type;
            hitPoints = snapshot.HitPoints;
            transform.position = snapshot.Position;
            transform.eulerAngles = snapshot.Rotation;
        }
    }
}