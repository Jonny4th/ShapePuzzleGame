﻿using Touch;
using UnityEngine;

namespace Assets.Scripts
{
    public class ArcAndStraightDebug : MonoBehaviour
    {
        [SerializeField]
        Object _swipeProcessor;

        ITouchDetection drag => _swipeProcessor as ITouchDetection;

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