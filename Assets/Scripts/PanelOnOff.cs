using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class PanelOnOff : MonoBehaviour
{
    [SerializeField] private bool _panelOn;
    public bool PanelOn
    {
        get
        {
            return _panelOn;
        }
    }
    #if UNITY_EDITOR
    private void Update() {
        CheckPanelState();
    }
    #endif
    [SerializeField] private bool _blockOn;
    public bool BlockOn
    {
        get
        {
            return _blockOn;
        }
        set
        {
            if (value != _blockOn)
            {
                _blockOn = value;
                OnChange?.Invoke();
            }
        }
    }
    public event Action OnChange;
    public event Action OnCorrect;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material shadowMaterial;
    [SerializeField] private Material correctMaterial;

    private void Start() {
        this.OnChange += CheckPanelState;
    }
    
    private void OnTriggerExit(Collider other)
    {
        BlockOn = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block")) BlockOn = true;
    }

    private void CheckPanelState()
    {
        //compound bools
        bool correct = _blockOn && _panelOn;
        bool needFill= !_blockOn && _panelOn;
        bool shadow  = _blockOn && !_panelOn;
        bool normal  = !_blockOn && !_panelOn;

        if (needFill)
        {
            gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else if (normal)
        {
            gameObject.GetComponent<MeshRenderer>().material = inactiveMaterial;
        }
        else if (shadow)
        {
            gameObject.GetComponent<MeshRenderer>().material = shadowMaterial;
        }
        else if (correct)
        {
            gameObject.GetComponent<MeshRenderer>().material = correctMaterial;
        }
    }
}
