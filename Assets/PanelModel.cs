using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelModel : MonoBehaviour
{
    public Vector3 coordinate;

    private void Awake()
    {
        coordinate = gameObject.transform.position;
    }
}
