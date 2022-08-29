using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardYAxis : MonoBehaviour
{
    GameController gameController;
    private void OnEnable() {
        gameController = GetComponent<GameController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.PageUp))
        {
            gameController.currentShape.transform.position += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.PageDown))
        {
            gameController.currentShape.transform.position -= Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameController.currentShape.transform.Rotate(Vector3.up, 90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameController.currentShape.transform.Rotate(Vector3.up, -90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameController.currentShape.transform.Rotate(Vector3.right, -90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameController.currentShape.transform.Rotate(Vector3.right, 90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameController.currentShape.transform.Rotate(Vector3.forward, -90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameController.currentShape.transform.Rotate(Vector3.forward, 90f, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameController.currentShape.transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameController.currentShape.transform.position -= Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameController.currentShape.transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameController.currentShape.transform.position -= Vector3.forward;
        }
    }
}
