using Shape.Inputs;
using Shape.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PlayerInput m_PlayerInput = null!;

    [SerializeField] private ObjectSelect m_ObjectSelect = null!;

    [SerializeField] private ShapeMovementManager m_ShapeMovementManager = null!;

    [SerializeField] private ShapeInputHandler m_ShapeMovementInputHandler = null!;

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
