using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeOverlapController : MonoBehaviour
{
    [SerializeField] BlockCollisionDetection[] blocks;
    [SerializeField] private bool isOverlap;
    public bool IsOverlap
    {
        get { return isOverlap; }
        private set { isOverlap = value; }
    }
    public event Action<bool> OverlapChanged;

    private void Awake()
    {
        blocks = GetComponentsInChildren<BlockCollisionDetection>();
    }

    public void CheckOverlap()
    {
        bool cache = Array.Exists(blocks, x => x.IsOverlap);
        if(cache != IsOverlap)
        {
            isOverlap = cache;
            OverlapChanged?.Invoke(isOverlap);
        }
    }
}
