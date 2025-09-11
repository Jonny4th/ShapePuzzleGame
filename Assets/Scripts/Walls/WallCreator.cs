using System;
using System.Collections.Generic;
using UnityEngine;

namespace Walls
{
    public class WallCreator : WallCreatable
    {
        [SerializeField]
        private GameObject m_TilePrototype;

        [SerializeField]
        private int m_Width;

        [SerializeField]
        private int m_Height;

        [SerializeField]
        private int m_Depth;

        [SerializeField]
        private Transform m_Parent;

        public override List<WallTileInfo> TileInfos => m_TileInfo;
        private List<WallTileInfo> m_TileInfo = new();

        public override WallCreatable SetTilePrototype(GameObject tilePrototype)
        {
            m_TilePrototype = tilePrototype;
            return this;
        }

        public override WallCreatable SetParent(Transform parent)
        {
            m_Parent = parent;
            return this;
        }

        public override WallCreatable SetDimention(int width, int height, int depth = 0)
        {
            m_Width = width;
            m_Height = height;
            m_Depth = depth;
            return this;
        }

        public override WallCreatable Build()
        {
            m_TileInfo.Clear();

            if(!Validate()) return null;

            DoTiling();

            return this;
        }

        public override WallCreatable Clear()
        {
            foreach(var tileInfo in m_TileInfo)
            {
                DestroyImmediate(tileInfo.Tile);
            }

            m_TileInfo.Clear();

            return this;
        }

        private void DoTiling()
        {
            for(var j = 0; j < m_Height; j++)
            {
                for(var i = 0; i < m_Width; i++)
                {
                    var x = i - (m_Width / 2f - 0.5f);
                    var y = j - (m_Height / 2f - 0.5f);

                    var tile = Instantiate(m_TilePrototype, m_Parent);
                    tile.transform.localPosition = new Vector3(x, y, m_Depth);
                    tile.transform.localRotation = Quaternion.identity;

                    var tileInfo = new WallTileInfo()
                    {
                        x = x,
                        y = y,
                        z = m_Depth,
                        Tile = tile,
                    };

                    m_TileInfo.Add(tileInfo);
                }
            }
        }

        private bool Validate()
        {
            if(m_Parent == null)
            {
                m_Parent = transform;
            }

            if(m_TilePrototype == null)
            {
                throw new MissingComponentException($"No tile prototype found. Please assign one using {nameof(SetTilePrototype)}");
            }

            if(m_Width <= 0 || m_Height <= 0)
            {
                throw new Exception($"Dimention must be positive interger, but you set them to: width = {m_Width}, height = {m_Height}");
            }

            return true;
        }
    }
}