using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Walls
{
    public class WallManager : WallCreatable
    {
        [SerializeField] private bool m_IsPivotAtCenter = true;
        [SerializeField] private WallCreatable m_ZWallCreator;
        [SerializeField] private WallCreatable m_XWallCreator;
        [SerializeField] private WallCreatable m_YWallCreator;

        [SerializeField] TileableMono m_TilePrototype;

        [SerializeField] private Transform m_Parent;

        [SerializeField] private int m_XSize;

        [SerializeField] private int m_YSize;

        [SerializeField] private int m_ZSize;

        public override List<TileableMono> TileInfos => m_TileInfos;
        private List<TileableMono> m_TileInfos = new();

        public void Awake()
        {
            m_TileInfos = new();

            if (m_TilePrototype != null)
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

            m_ZWallCreator.SetDimention(m_XSize, m_YSize, 0);
            m_XWallCreator.SetDimention(m_ZSize, m_YSize, 0);
            m_YWallCreator.SetDimention(m_XSize, m_ZSize, 0);
        }

        public override List<TileableMono> Build()
        {
            Clear();

            if(!(m_XSize == 0 || m_YSize == 0))
            {
                if(m_IsPivotAtCenter) m_ZWallCreator.transform.position = new Vector3(0, 0, m_ZSize / 2f + 1f);
                m_ZWallCreator.Build();
                m_TileInfos.AddRange(m_ZWallCreator.TileInfos);
            }

            if(!(m_ZSize == 0 || m_YSize == 0))
            {
                if(m_IsPivotAtCenter) m_XWallCreator.transform.position = new Vector3(m_XSize / 2f + 1f, 0, 0);
                m_XWallCreator.Build();
                m_TileInfos.AddRange(m_XWallCreator.TileInfos);
            }
            if(!(m_XSize == 0 || m_ZSize == 0))
            {
                if(m_IsPivotAtCenter) m_YWallCreator.transform.position = new Vector3(0, - m_ZSize / 2f - 1f, 0);
                m_YWallCreator.Build();
                m_TileInfos.AddRange(m_YWallCreator.TileInfos);
            }

            return m_TileInfos;
        }

        public override WallCreatable Clear()
        {
            m_TileInfos.Clear();
            m_ZWallCreator.Clear();
            m_XWallCreator.Clear();
            m_YWallCreator.Clear();

            return this;
        }

        public override WallCreatable SetDimention(Vector3Int dimention)
        {
            m_XSize = dimention.x;
            m_YSize = dimention.y;
            m_ZSize = dimention.z;

            m_ZWallCreator.SetDimention(m_XSize, m_YSize);
            m_XWallCreator.SetDimention(m_ZSize, m_YSize);
            m_YWallCreator.SetDimention(m_XSize, m_ZSize);

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

        public override WallCreatable SetTilePrototype(TileableMono tile)
        {
            m_TilePrototype = tile;

            m_ZWallCreator.SetTilePrototype(tile);
            m_XWallCreator.SetTilePrototype(tile);
            m_YWallCreator.SetTilePrototype(tile);

            return this;
        }

    }
}