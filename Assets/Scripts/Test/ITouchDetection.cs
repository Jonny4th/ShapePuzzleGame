using System;
using UnityEngine;

public interface ITouchDetection
{
    event Action<float> ArcDetected;
    event Action<Vector2> StraightDetected;
}