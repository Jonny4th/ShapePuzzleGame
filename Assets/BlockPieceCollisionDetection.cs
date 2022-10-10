using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPieceCollisionDetection : MonoBehaviour
{
    //public event Action OnCollisionEnter;
    //public event Action OnCollisionExit;

    private bool isOverlap;
    [SerializeField] LayerMask mask;

    #region MonoBehaviors
    ShapeController shapeController;
    private void Awake()
    {
        shapeController = GetComponentInParent<ShapeController>();

    }
    private void OnEnable()
    {
        shapeController.OnShapeSelect += SelectResponse;
        shapeController.OnShapeDeselect += DeselectResponse;
        shapeController.OnBlockOverlap += BlockCollisionResponse;
        ShapeMovementManager.OnMovement += CheckBlockOverlap;
    }

    private void CheckBlockOverlap(OnMovementInfo info)
    {
        if (info.shape == shapeController)
        {
            Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2,Quaternion.identity, mask, QueryTriggerInteraction.Ignore);
            hitColliders = Array.FindAll(hitColliders, x => x.GetComponentInParent<ShapeController>() != shapeController);
            
            //int i = 0;
            //while (i < hitColliders.Length)
            //{
            //    ShapeController collider = hitColliders[i].GetComponentInParent<ShapeController>();
            //    Debug.Log("Hit : " + collider.name);
            //    i++;
            //}
        }
    }

    private void OnDisable()
    {
        shapeController.OnShapeSelect -= SelectResponse;
        shapeController.OnShapeDeselect -= DeselectResponse;
        shapeController.OnBlockOverlap -= BlockCollisionResponse;
    }
    #endregion

    private void DeselectResponse()
    {
        //GetComponent<Collider>().isTrigger = false;
    }

    private void SelectResponse()
    {
        //GetComponent<Collider>().isTrigger = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.TryGetComponent<BlockPieceCollisionDetection>(out BlockPieceCollisionDetection _other))
    //    {
    //        //OnCollisionEnter?.Invoke();
    //        ToggleIsOverlap(true);
    //        shapeController.BlockOverlap(true);
    //        Debug.Log(message: _other.GetComponentInParent<ShapeController>().name);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    ToggleIsOverlap(false);
    //    shapeController.BlockOverlap(false);
    //    //OnCollisionExit?.Invoke();
    //}

    private void ToggleIsOverlap(bool valid)
    {
        if (isOverlap != valid)
        {
            isOverlap = valid;
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
