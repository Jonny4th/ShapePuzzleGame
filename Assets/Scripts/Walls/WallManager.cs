using System.Collections.Generic;
using UnityEngine;

namespace Walls
{
    public class WallManager : WallCreatable
    {
        [SerializeField]
        private WallCreatable m_ZWallCreator;

        [SerializeField]
        private WallCreatable m_XWallCreator;

        [SerializeField]
        private WallCreatable m_YWallCreator;

        [SerializeField]
        GameObject m_TilePrototype;

        [SerializeField]
        private Transform m_Parent;

        [SerializeField]
        private int m_X;

        [SerializeField]
        private int m_Y;

        [SerializeField]
        private int m_Z;

        public override List<WallTileInfo> TileInfos => m_TileInfos;
        private List<WallTileInfo> m_TileInfos = new();

        public void Awake()
        {
            if(m_TilePrototype != null)
            {
                m_ZWallCreator.SetTilePrototype(m_TilePrototype);
                m_XWallCreator.SetTilePrototype(m_TilePrototype);
                m_YWallCreator.SetTilePrototype(m_TilePrototype);
            }

            if(m_Parent != null)
            {
                m_ZWallCreator.SetParent(m_Parent);
                m_XWallCreator.SetParent(m_Parent);
                m_YWallCreator.SetParent(m_Parent);
            }

            m_ZWallCreator.SetDimention(m_X, m_Y, 0);
            m_XWallCreator.SetDimention(m_Z, m_Y, 0);
            m_YWallCreator.SetDimention(m_X, m_Z, 0);
        }

        public override WallCreatable Build()
        {
            m_TileInfos.Clear();

            if(!(m_X == 0 || m_Y == 0))
            {
                m_ZWallCreator.transform.position = new Vector3(0, 0, m_Z / 2f + 1f);
                m_ZWallCreator.Build();
                m_TileInfos.AddRange(m_ZWallCreator.TileInfos);
            }

            if(!(m_Z == 0 || m_Y == 0))
            {
                m_XWallCreator.transform.position = new Vector3(m_X / 2f + 1f, 0, 0);
                m_XWallCreator.Build();
                m_TileInfos.AddRange(m_XWallCreator.TileInfos);
            }
            if(!(m_X == 0 || m_Z == 0))
            {
                m_YWallCreator.transform.position = new Vector3(0, - m_Z / 2f - 1f, 0);
                m_YWallCreator.Build();
                m_TileInfos.AddRange(m_YWallCreator.TileInfos);
            }

            return this;
        }

        public override WallCreatable Clear()
        {
            m_TileInfos.Clear();
            m_ZWallCreator.Clear();
            m_XWallCreator.Clear();
            m_YWallCreator.Clear();

            return this;
        }

        public override WallCreatable SetDimention(int x, int y, int z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;

            m_ZWallCreator.SetDimention(x, y);
            m_XWallCreator.SetDimention(z, y);
            m_YWallCreator.SetDimention(x, z);

            return this;
        }

        public override WallCreatable SetParent(Transform parent)
        {
            m_Parent = parent;

            m_ZWallCreator.SetParent(parent);
            m_XWallCreator.SetParent(parent);
            m_YWallCreator.SetParent(parent);

            return this;
        }

        public override WallCreatable SetTilePrototype(GameObject tile)
        {
            m_TilePrototype = tile;

            m_ZWallCreator.SetTilePrototype(tile);
            m_XWallCreator.SetTilePrototype(tile);
            m_YWallCreator.SetTilePrototype(tile);

            return this;
        }
    }
}