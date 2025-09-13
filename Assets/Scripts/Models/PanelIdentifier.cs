using System;
using UnityEngine;

public enum PanelSide
{
    X,Y,Z
}

[Serializable]
public struct PanelIdentifier
{
    public PanelSide PanelSide;
    public Vector2Int Coordinate;

    public static bool operator ==(PanelIdentifier a, PanelIdentifier b)
    {
        return a.PanelSide == b.PanelSide &&
                a.Coordinate == b.Coordinate;
    }

    public static bool operator !=(PanelIdentifier a, PanelIdentifier b)
    {
        return !(a.PanelSide == b.PanelSide &&
                a.Coordinate == b.Coordinate);
    }
}
