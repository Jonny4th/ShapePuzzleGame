using System;
using UnityEngine;

public enum PanelSide
{
    X,Y,Z
}

[Serializable]
public struct TileIdentifier
{
    public PanelSide PanelSide;
    public Vector2Int Coordinate;

    public static bool operator ==(TileIdentifier a, TileIdentifier b)
    {
        return a.PanelSide == b.PanelSide &&
                a.Coordinate == b.Coordinate;
    }

    public static bool operator !=(TileIdentifier a, TileIdentifier b)
    {
        return !(a.PanelSide == b.PanelSide &&
                a.Coordinate == b.Coordinate);
    }
}
