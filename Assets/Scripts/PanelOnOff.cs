using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class PanelOnOff : MonoBehaviour
{

    [SerializeField] private bool _isOn;
    public bool IsOn
    {
        get
        {
            return _isOn;
        }
    }

    #if UNITY_EDITOR
    private void Update() {
        CheckPanelState(null);
    }
    #endif
    
    [SerializeField] private bool _isBlock;
    public bool IsBlock
    {
        get
        {
            return _isBlock;
        }
        set
        {
            if (value != _isBlock)
            {
                _isBlock = value;
            }
        }
    }

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material shadowMaterial;
    [SerializeField] private Material correctMaterial;

    private void OnEnable() {
        //ShapeMovementManager.OnMovement += CheckPanelState;
    }
    private void OnDisable() {
        //ShapeMovementManager.OnMovement -= CheckPanelState;
    }

    private void OnTriggerExit(Collider other)
    {
        IsBlock = false;
        CheckPanelState(null);
    }

    private void OnTriggerStay(Collider other)
    {
        ShapeOverlapController shape = other.GetComponentInParent<ShapeOverlapController>();
        if (shape != null)
        {
            IsBlock = !shape.IsOverlap;
            CheckPanelState(null);
        }
    }

    private void CheckPanelState(OnMovementInfo info)
    {
        //compound bools
        bool correct = _isBlock && _isOn;
        bool needFill= !_isBlock && _isOn;
        bool shadow  = _isBlock && !_isOn;
        bool normal  = !_isBlock && !_isOn;

        if (needFill)
        {
            GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else if (normal)
        {
            GetComponent<MeshRenderer>().material = inactiveMaterial;
        }
        else if (shadow)
        {
            GetComponent<MeshRenderer>().material = shadowMaterial;
        }
        else if (correct)
        {
            GetComponent<MeshRenderer>().material = correctMaterial;
        }
    }
}
