using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public interface IPointsVisualizable
    {
        event Action<List<Vector2>> LineUpdated;
        event Action<Vector2> PointUpdated;
    }
}
