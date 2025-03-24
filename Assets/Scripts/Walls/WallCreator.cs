using System;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TilePrototype;

    [SerializeField]
    private int m_Width;

    [SerializeField]
    private int m_Height;

    [SerializeField]
    private Transform m_Parent;

    [SerializeField]
    private List<GameObject> m_Tiles = new();
    public List<GameObject> Tiles => m_Tiles;

    public WallCreator SetTilePrototype(GameObject tilePrototype)
    {
        m_TilePrototype = tilePrototype;
        return this;
    }

    public WallCreator SetParent(Transform parent)
    {
        m_Parent = parent;
        return this;
    }

    public WallCreator SetDimention(int width, int height)
    {
        m_Width = width;
        m_Height = height;
        return this;
    }

    public WallCreator Create()
    {
        m_Tiles.Clear();

        if(!Validate()) return null;

        DoTiling();

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

                var tile = Instantiate(m_TilePrototype, new Vector3(x, y), Quaternion.identity, m_Parent);
                m_Tiles.Add(tile);
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

        if(m_Width == 0 || m_Height == 0)
        {
            throw new Exception($"Dimention must not contain 0. You set them to width: {m_Width}, height {m_Height}");
        }

        return true;
    }
}
