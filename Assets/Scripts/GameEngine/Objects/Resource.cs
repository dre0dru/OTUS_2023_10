using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Resource : MonoBehaviour
    {
        public struct Snapshot
        {
            public string Id;
            public int Amount;
        }

        public string ID
        {
            get => id;
        }

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        [SerializeField]
        private string id;

        [SerializeField]
        private int amount;

        public Snapshot GetSnapshot()
        {
            return new Snapshot()
            {
                Id = id,
                Amount = amount
            };
        }

        public void RestoreFromSnapshot(Snapshot snapshot)
        {
            id = snapshot.Id;
            Amount = snapshot.Amount;
        }
    }
}
