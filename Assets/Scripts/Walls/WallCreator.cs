using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Walls
{
    public class WallCreator : WallCreatable
    {
        [SerializeField] private bool m_IsPivotAtCenter = true;
        [SerializeField] private PanelSide panelSide;
        [SerializeField] private TileableMono m_TilePrototype;
        [SerializeField] private int m_Width;
        [SerializeField] private int m_Height;
        [SerializeField] private int m_Depth;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private List<TileableMono> m_TileInfo = new();
        public override List<TileableMono> TileInfos => m_TileInfo;

        public override WallCreatable SetTilePrototype(TileableMono tilePrototype)
        {
            m_TilePrototype = tilePrototype;
            return this;
        }

        public override WallCreatable SetParent(Transform parent)
        {
            m_Parent = parent;
            return this;
        }

        public override WallCreatable SetDimention(Vector3Int dimention)
        {
            m_Width = dimention.x;
            m_Height = dimention.y;
            m_Depth = dimention.z;

            return this;
        }

        public override List<TileableMono> Build()
        {
            m_TileInfo.Clear();

            if (!Validate()) return null;

            m_TileInfo = DoTiling();

            return m_TileInfo;
        }

        public override WallCreatable Clear()
        {
            if (m_TileInfo.Count == 0)
            {
                if(m_Parent == null) m_Parent = transform;

                m_TileInfo.AddRange(m_Parent.GetComponentsInChildren<TileableMono>());
            }

            foreach (var tileInfo in m_TileInfo)
            {
                if(tileInfo != null) DestroyImmediate(tileInfo.gameObject);
            }

            m_TileInfo.Clear();

            return this;
        }

        private List<TileableMono> DoTiling()
        {
            var tiles = new List<TileableMono>();
            var posCoef = m_IsPivotAtCenter ? 1f : 0f;
            
            for (var j = 0; j < m_Height; j++)
            {
                for (var i = 0; i < m_Width; i++)
                {
                    var xPos = i - posCoef * (m_Width / 2f - 0.5f);
                    var yPos = j - posCoef * (m_Height / 2f - 0.5f);

                    var tile = Instantiate(m_TilePrototype, m_Parent);
                    tile.transform.SetLocalPositionAndRotation(new Vector3(xPos, yPos, m_Depth), Quaternion.identity);
                    if(panelSide == PanelSide.Z) tile.transform.Rotate(Vector3.up, 180f);
                    tile.name = $"Panel ({i},{j})";
                    tile.Identifier = new()
                    {
                        PanelSide = panelSide,
                        Coordinate = new(i, j)
                    };
                    tiles.Add(tile);
                }
            }

            return tiles;
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