using UnityEngine;

public class BlockPanelTrigger : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    void Start()
    {
        colliders = GetComponents<Collider>();
    }

}
