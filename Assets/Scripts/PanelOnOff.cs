using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PanelOnOff : MonoBehaviour
{
    public bool panelOn;
    public bool blockOn;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material shadowMaterial;
    [SerializeField] private Material correctMaterial;

    private void Update()
    {
        CheckPanelState();
    }
    private void OnTriggerExit(Collider other)
    {
        blockOn = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block")) blockOn = true;
    }


    private void CheckPanelState()
    {
        //compound bools
        bool correct = blockOn && panelOn;
        bool needFill= !blockOn && panelOn;
        bool shadow  = blockOn && !panelOn;
        bool normal  = !blockOn && !panelOn;

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
