using UnityEngine;

public class SuccessScreen : MonoBehaviour
{
    [SerializeField] GameObject successScreen;
    public bool successScreenOn { get; private set; }
    void Awake()
    {
        successScreen.SetActive(false);
        successScreenOn = false;
        Time.timeScale = 1f;
    }

    public void ShowScreen()
    {
        Time.timeScale = 0f;
        successScreen.SetActive(true);
        successScreenOn = true;
    }
}
