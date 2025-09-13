using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Walls
{
    public struct WallTileInfo
    {
        public float x;
        public float y;
        public float z;
        public TileableMono Tile;
    }

    public abstract class WallCreatable : MonoBehaviour
    {
        public abstract List<TileableMono> TileInfos { get; }
        public abstract WallCreatable SetTilePrototype(TileableMono tile);
        public abstract WallCreatable SetParent(Transform parent);
        public virtual WallCreatable SetDimention(int x, int y, int z = 0)
        {
            return SetDimention(new(x, y, z));
        }

        public abstract WallCreatable SetDimention(Vector3Int dimention);
        public abstract List<TileableMono> Build();
        public abstract WallCreatable Clear();
    }
}