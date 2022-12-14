using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
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
    public void OnPause()
    {
        playerInput.SwitchCurrentActionMap("Menu");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
