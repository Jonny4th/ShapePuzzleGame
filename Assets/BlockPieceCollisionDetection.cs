using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPieceCollisionDetection : MonoBehaviour
{
    //public event Action OnCollisionEnter;
    //public event Action OnCollisionExit;

    public bool IsOverlap { get; private set; }
    [SerializeField] LayerMask mask;

    #region MonoBehaviors
    ShapeOverlapController shapeController;
    private void Awake()
    {
        shapeController = GetComponentInParent<ShapeOverlapController>();
    }

    private void OnEnable()
    {
        ShapeMovementManager.OnMovement += CheckBlockOverlap;
    }


    private void OnDisable()
    {
        ShapeMovementManager.OnMovement -= CheckBlockOverlap;
    }
    #endregion

    private void CheckBlockOverlap(OnMovementInfo info)
    {
        if (info.shape == shapeController)
        {
            Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2,Quaternion.identity, mask, QueryTriggerInteraction.Ignore);
            hitColliders = Array.FindAll(hitColliders, x => x.GetComponentInParent<ShapeOverlapController>() != shapeController);
            
        }
    }
    private void ToggleIsOverlap(bool value)
    {
        if (IsOverlap != value)
        {
            IsOverlap = value;
        }
    }

    private void BlockCollisionResponse(bool stay)
    {
        if(stay)
        {

        }
        else
        {

        }
    }

}
