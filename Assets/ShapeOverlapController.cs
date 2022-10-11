using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeOverlapController : MonoBehaviour
{
    [SerializeField] BlockCollisionDetection[] blocks;
    public bool IsOverlap { get; private set; }
    public event Action<bool> OnOverlapChanged;

    private void Awake()
    {
        blocks = GetComponentsInChildren<BlockCollisionDetection>();
    }

    public void CheckOverlap()
    {
        bool cache = Array.Exists(blocks, x => x.IsOverlap);
        if(cache != IsOverlap)
        {
            IsOverlap = cache;
            OnOverlapChanged?.Invoke(IsOverlap);
        }
    }
}
