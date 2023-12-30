using System;
using System.Collections.Generic;
using GameEngine;

namespace SaveSystem
{
    [Serializable]
    public struct ResourcesData
    {
        public Dictionary<string, Resource.Snapshot> Resources;
    }
}
