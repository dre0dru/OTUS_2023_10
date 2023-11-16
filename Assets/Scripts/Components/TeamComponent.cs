using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public bool IsPlayer
        {
            get { return this.isPlayer; }
        }
        
        [SerializeField]
        private bool isPlayer;
    }
}