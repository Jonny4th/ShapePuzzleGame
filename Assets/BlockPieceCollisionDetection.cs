using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPieceCollisionDetection : MonoBehaviour
{
    public bool IsOverlap { get; private set; }
    public event Action OnCollisionDetected;
    [SerializeField] LayerMask mask;
    ShapeOverlapController shapeController;

    #region MonoBehaviors
    private void Awake()
    {
        shapeController = GetComponentInParent<ShapeOverlapController>();
    }
    private void OnEnable()
    {
        ShapeMovementManager.OnMovement += CheckBlockOverlap;
        shapeController.IsOverlapChanged += BlockCollisionResponse;
    }
    private void OnDisable()
    {
        ShapeMovementManager.OnMovement -= CheckBlockOverlap;
        shapeController.IsOverlapChanged -= BlockCollisionResponse;
    }
    #endregion

    private void CheckBlockOverlap(OnMovementInfo info)
    {
        //if (info.shape == shapeController.gameObject)
        //{
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2,Quaternion.identity, mask, QueryTriggerInteraction.Ignore);
        hitColliders = Array.FindAll(hitColliders, x => x.GetComponentInParent<ShapeOverlapController>() != shapeController);
        ToggleIsOverlap(hitColliders.Length > 0);
        //}
    }
    private void ToggleIsOverlap(bool value)
    {
        if (IsOverlap != value)
        {
            IsOverlap = value;
            OnCollisionDetected?.Invoke();
        }
    }

    private void BlockCollisionResponse(bool stay)
    {
        if(stay)
        {
            Debug.Log("Overlap");
        }
        else
        {
            Debug.Log("Out");

        }
    }

}
