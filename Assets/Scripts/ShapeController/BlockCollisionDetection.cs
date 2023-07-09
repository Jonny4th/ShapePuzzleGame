using System;
using UnityEngine;
using Shape.Movement;
using Shape.Controller;

public class BlockCollisionDetection : MonoBehaviour
{
    [SerializeField] private bool isOverlap;
    public bool IsOverlap
    {
        get { return isOverlap; }
        private set { isOverlap = value; }
    }
    [SerializeField] LayerMask mask;
    ShapeOverlapController overlapController;
    [SerializeField] Material invalidMaterial;

    #region MonoBehaviors
    private void Awake()
    {
        overlapController = GetComponentInParent<ShapeOverlapController>();
    }

    private void Update()
    {
        CheckBlockOverlap(null);
    }
    #endregion

    private void CheckBlockOverlap(MovementInfo info)
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, mask, QueryTriggerInteraction.Ignore);
        hitColliders = Array.FindAll(hitColliders, x => x.GetComponentInParent<ShapeOverlapController>() != overlapController);
        ToggleIsOverlap(hitColliders.Length > 0);
    }
    private void ToggleIsOverlap(bool value)
    {
        if(IsOverlap != value)
        {
            isOverlap = value;
            overlapController.CheckOverlap();
        }
    }
}
