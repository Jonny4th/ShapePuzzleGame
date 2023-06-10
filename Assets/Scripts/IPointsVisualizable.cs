using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IPointsVisualizable
    {
        event Action<List<Vector2>> LineUpdated;
        event Action<Vector2> PointUpdated;
    }
}
