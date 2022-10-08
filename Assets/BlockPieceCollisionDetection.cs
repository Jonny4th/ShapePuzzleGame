using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class BlockPieceCollisionDetection : MonoBehaviour
{
    public event Action OnCollisionEnter;
    public event Action OnCollisionExit;

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
    }
    private void OnDisable()
    {
        shapeController.OnShapeSelect -= SelectResponse;
        shapeController.OnShapeDeselect -= DeselectResponse;
    }
    #endregion

    private void DeselectResponse()
    {
        GetComponent<Collider>().isTrigger = false;
    }

    private void SelectResponse()
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

}
