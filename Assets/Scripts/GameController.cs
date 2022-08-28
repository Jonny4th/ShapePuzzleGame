using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject currentGameobject;
    public ShapeController currentShape;

    private void Awake()
    {
        ObjectSelect.OnShapeSelect += ShapeSelectResponse;
        ObjectSelect.OnShapeDeselect += Clear;
    }
    private void ShapeSelectResponse(GameObject seleted)
    {
        currentGameobject = seleted;
        currentShape = currentGameobject.transform.parent.GetComponent<ShapeController>();
    }

    private void Clear()
    {
        currentGameobject = null;
        currentShape = null;
    }

}
