using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject currentGameobject;
    public ShapeController currentShape;
    public List<ShapeController> ShapeInScene;
    private void Awake()
    {
        ObjectSelect.OnShapeSelect += ShapeSelectResponse;
        ObjectSelect.OnShapeDeselect += Clear;
    }
    private void Start()
    {
        ShapeInScene.AddRange(GameObject.FindObjectsOfType<ShapeController>());
    }
    private void ShapeSelectResponse(GameObject seleted)
    {
        currentGameobject = seleted;
        currentShape = currentGameobject.GetComponentInParent<ShapeController>();
    }

    private void Clear()
    {
        currentGameobject = null;
        currentShape = null;
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
