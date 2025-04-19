using Shape.Inputs;
using Shape.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Walls;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerInput m_PlayerInput;

    [SerializeField]
    ObjectSelect m_ObjectSelect;

    [SerializeField]
    ShapeMovementManager m_ShapeMovementManager;

    [SerializeField]
    ShapeInputHandler m_ShapeMovementInputHandler;

    [SerializeField]
    WallCreatable m_WallCreator;

    void Awake()
    {
        m_ShapeMovementInputHandler.SetShapeMovementController(m_ShapeMovementManager);
    }

    private void OnEnable()
    {
        m_ObjectSelect.MovementHandlerSelected.AddListener(HandelShapeSelected);
        m_ObjectSelect.ShapeDeselected.AddListener(HandleShapeDeselected);

        OnPlay();
    }

    private void OnDisable()
    {
        m_ObjectSelect.MovementHandlerSelected.RemoveListener(HandelShapeSelected);
        m_ObjectSelect.ShapeDeselected.RemoveListener(HandleShapeDeselected);

        OnPlay();
    }

    private void HandleShapeDeselected()
    {
        m_ShapeMovementManager.ClearSelectedShape();
    }

    private void HandelShapeSelected(IMotionInfo shape)
    {
        m_ShapeMovementManager.AssignSelectedShape(shape);
    }

    public void OnGameOver()
    {
        m_PlayerInput.SwitchCurrentActionMap("GameOverMenu");
    }
    public void OnPlay()
    {
        m_PlayerInput.SwitchCurrentActionMap("Puzzle Controls");
    }
    public void OnPause()
    {
        m_PlayerInput.SwitchCurrentActionMap("Menu");
    }

    public void OnResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
