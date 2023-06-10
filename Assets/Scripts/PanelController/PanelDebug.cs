using UnityEngine;

public class PanelDebug : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
    }
}
