using UnityEngine;

namespace Scripts.Walls
{
    public abstract class TileableMono : MonoBehaviour
    {
        public abstract TileIdentifier Identifier { get; set; }
    }
}
