using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPanelTrigger : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    void Start()
    {
        colliders = GetComponents<Collider>();
    }

}
