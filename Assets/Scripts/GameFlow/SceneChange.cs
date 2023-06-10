using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] int toSceneIndex;
    [SerializeField] string toSceneName;
    [SerializeField] bool useSceneName;
    [SerializeField] bool goToNextScene;
    public void ChangeScene()
    {
        if(goToNextScene)
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            if(useSceneName) SceneManager.LoadScene(toSceneName);
            else SceneManager.LoadScene(toSceneIndex);
        }
    }
}
