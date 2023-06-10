using UnityEngine;

public class SuccessScreen : MonoBehaviour
{
    [SerializeField] GameObject successScreen;
    public bool successScreenOn { get; private set; }
    void Awake()
    {
        GameOverManager.Success += ShowScreen;
        successScreen.SetActive(false);
        successScreenOn = false;
        Time.timeScale = 1f;
    }
    private void OnDisable()
    {
        GameOverManager.Success -= ShowScreen;
    }
    private void ShowScreen()
    {
        Time.timeScale = 0f;
        successScreen.SetActive(true);
        successScreenOn = true;
    }
}
