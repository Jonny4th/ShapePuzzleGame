using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        OnPlay();
    }
    private void OnDisable()
    {
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

    public void OnResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
