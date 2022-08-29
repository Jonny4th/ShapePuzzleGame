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
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameController.currentShape.transform.position += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameController.currentShape.transform.position -= Vector3.up;
        }
    }
}
