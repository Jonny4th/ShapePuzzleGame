using UnityEngine;

namespace Scripts.Walls
{
    public abstract class TileableMono : MonoBehaviour
    {
        public abstract PanelIdentifier Identifier { get; set; }
    }
}
