using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeOverlapController : MonoBehaviour
{
    [SerializeField] BlockPieceCollisionDetection[] blocks;
    public bool IsOverlap { get; private set; }
    public event Action<Boolean> IsOverlapChanged;

    private void Awake()
    {
        blocks = GetComponentsInChildren<BlockPieceCollisionDetection>();
    }
    private void OnEnable()
    {
        foreach (BlockPieceCollisionDetection block in blocks)
        {
            block.OnCollisionDetected += CheckOverlap;
        }
    }

    private void CheckOverlap()
    {
        bool cache = Array.Exists(blocks, x => x.IsOverlap);
        if(cache != IsOverlap)
        {
            IsOverlap = cache;
            IsOverlapChanged?.Invoke(IsOverlap);
        }
    }
}
