using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionDetection : MonoBehaviour
{
    public bool IsOverlap { get; private set; }
    [SerializeField] LayerMask mask;
    ShapeOverlapController overlapController;
    [SerializeField] Material invalidMaterial;

    #region MonoBehaviors
    private void Awake()
    {
        overlapController = GetComponentInParent<ShapeOverlapController>();
        
    }
    private void OnEnable()
    {
        //ShapeMovementManager.OnMovement += CheckBlockOverlap;
        //overlapController.OnOverlapChanged += BlockCollisionResponse;
    }
    private void OnDisable()
    {
        //ShapeMovementManager.OnMovement -= CheckBlockOverlap;
        //overlapController.OnOverlapChanged -= BlockCollisionResponse;
    }
    private void Update()
    {
        CheckBlockOverlap(null);
    }
    #endregion

    private void CheckBlockOverlap(OnMovementInfo info)
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2,Quaternion.identity, mask, QueryTriggerInteraction.Ignore);
        hitColliders = Array.FindAll(hitColliders, x => x.GetComponentInParent<ShapeOverlapController>() != overlapController);
        ToggleIsOverlap(hitColliders.Length > 0);
    }
    private void ToggleIsOverlap(bool value)
    {
        if (IsOverlap != value)
        {
            IsOverlap = value;
            overlapController.CheckOverlap();
        }
    }

    //private void BlockCollisionResponse(bool stay)
    //{
    //    if(stay)
    //    {
    //        GetComponent<MeshRenderer>().material = invalidMaterial;
    //    }
    //    else
    //    {
    //        GetComponent<MeshRenderer>().material = GetComponentInParent<ShapeModel>().Data.OriginalMaterial;
    //    }
    //}
}
