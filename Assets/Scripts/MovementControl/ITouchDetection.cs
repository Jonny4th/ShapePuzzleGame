using System;
using UnityEngine;

namespace Touch
{
    public interface ITouchDetection
    {
        event Action<float> ArcDetected;
        event Action<Vector2> StraightDetected;
    }

}