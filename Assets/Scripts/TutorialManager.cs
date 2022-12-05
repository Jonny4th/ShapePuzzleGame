using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TutorialControl control;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ShowTutorial;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ShowTutorial;
    }

    private void ShowTutorial(Scene scene, LoadSceneMode mode)
    {
        control.gameObject.SetActive(true);
        int n = scene.buildIndex;
        control.tutorials[n].gameObject.SetActive(true);
    }

    private void HideTutorial()
    {

    }
}
