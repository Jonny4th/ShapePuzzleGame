using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    PlayerInput playerInput;

    private void OnEnable()
    {
        GameOverManager.Success += OnGameOver;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
    }
    private void OnDisable()
    {
        GameOverManager.Success -= OnGameOver;
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
    }

    public void OnGameOver()
    {
        playerInput.SwitchCurrentActionMap("GameOverMenu");
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
