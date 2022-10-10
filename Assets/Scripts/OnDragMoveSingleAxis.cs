using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDragMoveSingleAxis : MonoBehaviour
{
    ShapeSelectionController parent;
    Camera mainCamera;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 pos;
    [SerializeField] LayerMask xzplane;
    [SerializeField] LayerMask yplane;
    GameController gameController;
    GameObject xzPlane;
    private void OnEnable() {
        parent = GetComponentInParent<ShapeSelectionController>();
        mainCamera = Camera.main;
        gameController = FindObjectOfType<GameController>();
        xzPlane = FindObjectOfType<xzPlane>(true).gameObject;
    }

    private void OnMouseDown() {
        //get offset
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, LayerMask.GetMask("Block")))
        {
            xzPlane.transform.position = new Vector3(0,hit.point.y,0);
            xzPlane.SetActive(true);
        }
        // parent.xzplane.SetActive(true);
        if (Physics.Raycast(ray, out RaycastHit planeHit, 20f, xzplane))
        {
            pos = parent.transform.position;
            offset = pos - planeHit.point;
        }
    }
    private void OnMouseDrag() {
        //update hit.point position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, xzplane, QueryTriggerInteraction.Ignore))
        {
            // Debug.Log("hit xy plane");
            pos = (hit.point + offset);
            pos.y = parent.transform.position.y;
            parent.transform.position = SnapToGrid(pos);
            // xzPlane.transform.position = parent.transform.position;
        }
    }
    private void OnMouseUp() {
        //disable plane
        // parent.xzplane.SetActive(false);
        xzPlane.SetActive(false);
    }

    private Vector3 SnapToGrid(Vector3 v)
    {
        var x = Mathf.Round(v.x);
        var y = Mathf.Round(v.y);
        var z = Mathf.Round(v.z);
        return new Vector3(x,y,z);
    }
}
