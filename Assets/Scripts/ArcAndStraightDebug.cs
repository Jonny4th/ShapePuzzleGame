using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ArcAndStraightDebug : MonoBehaviour
    {
        [SerializeField]
        DragDetect drag;

        void OnEnable()
        {
            drag.StraightDetected += OnStraight;
            drag.ArcDetected += OnArc;
        }

        private void OnArc(float direction)
        {
            Debug.Log(direction);
        }

        private void OnStraight(Vector2 direction)
        {
            Debug.Log(direction);
        }
    }
}