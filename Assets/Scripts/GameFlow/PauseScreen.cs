using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPause;

    public void Pause()
    {
        if(!GameOverManager.gameIsOver)
        {
            Time.timeScale = 0f;
            GameIsPause = true;
            gameObject.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPause = false;
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
