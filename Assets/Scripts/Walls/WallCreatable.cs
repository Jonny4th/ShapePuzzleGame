using System.Collections.Generic;
using UnityEngine;

namespace Walls
{
    public struct WallTileInfo
    {
        public float x;
        public float y;
        public float z;
        public GameObject Tile;
    }

    public abstract class WallCreatable : MonoBehaviour
    {
        public abstract List<WallTileInfo> TileInfos { get; }
        public abstract WallCreatable SetTilePrototype(GameObject tile);
        public abstract WallCreatable SetParent(Transform parent);
        public abstract WallCreatable SetDimention(int x, int y, int z = 0);
        public abstract WallCreatable Build();
        public abstract WallCreatable Clear();
    }
}