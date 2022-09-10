using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public GameObject currentGameobject;
    public ShapeController currentShape;
    PlayerInput playerInput;
    private void OnEnable()
    {
        ObjectSelect.OnShapeSelect += ShapeSelectResponse;
        ObjectSelect.OnShapeDeselect += Clear;
        GameOverManager.Success += OnMenu;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
    }
    private void OnDisable()
    {
        ObjectSelect.OnShapeSelect -= ShapeSelectResponse;
        ObjectSelect.OnShapeDeselect -= Clear;
        GameOverManager.Success -= OnMenu;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
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

    public void OnMenu()
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
