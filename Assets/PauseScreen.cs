using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPause;
    public GameObject pauseMenuUI;
    public void Pause()
    {
        if(!GameOverManager.gameIsOver)
        {
            Time.timeScale = 0f;
            GameIsPause = true;
            pauseMenuUI.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPause = false;
        pauseMenuUI.SetActive(false);
    }
}
