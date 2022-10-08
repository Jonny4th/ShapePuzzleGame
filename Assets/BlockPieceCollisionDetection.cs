using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class BlockPieceCollisionDetection : MonoBehaviour
{
    public event Action OnCollisionEnter;
    public event Action OnCollisionExit;
    BlockPieceSelect parentBlock;

    private void OnEnable()
    {
        parentBlock = GetComponent<BlockPieceSelect>();
        parentBlock.Selected += SelectResponce;
        parentBlock.Deselected += DeselectResponce;
    }
    private void OnDisable()
    {
        parentBlock.Selected -= SelectResponce;
        parentBlock.Deselected -= DeselectResponce;
    }

    private void DeselectResponce()
    {
        GetComponent<Collider>().isTrigger = false;
    }

    private void SelectResponce()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionEnter?.Invoke();
        Debug.Log("collide");
    }

    private void OnTriggerExit(Collider other)
    {
        OnCollisionExit?.Invoke();
    }

    //public void InvalidResponce()
    //{
    //    originalMaterial = GetComponentInParent<MeshRenderer>().material;
    //    GetComponentInParent<MeshRenderer>().material = invalidMaterial;
    //    foreach (BoxCollider collider in GetComponentsInParent<BoxCollider>())
    //    {
    //        if (collider.isTrigger)
    //        {
    //            collider.enabled = false;
    //        }
    //    }
    //    OnValidityChange?.Invoke();
    //}

}
