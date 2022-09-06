using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public GameObject currentGameobject;
    public ShapeController currentShape;
    PlayerInput playerInput;
    private void Awake()
    {
        ObjectSelect.OnShapeSelect += ShapeSelectResponse;
        ObjectSelect.OnShapeDeselect += Clear;
        playerInput = GetComponent<PlayerInput>();
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

    public void OnPause()
    {
        playerInput.SwitchCurrentActionMap("Menu");
    }
    public void OnPlay()
    {
        playerInput.SwitchCurrentActionMap("Puzzle Controls");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
