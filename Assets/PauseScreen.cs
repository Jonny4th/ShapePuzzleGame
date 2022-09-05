using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPause;
    public GameObject pauseMenuUI;
    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPause = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPause = false;
        pauseMenuUI.SetActive(false);
    }
}
