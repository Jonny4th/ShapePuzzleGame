using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    PlayerInput playerInput;

    private void OnEnable()
    {
        GameOverManager.Success += OnMenu;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
    }
    private void OnDisable()
    {
        GameOverManager.Success -= OnMenu;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
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
