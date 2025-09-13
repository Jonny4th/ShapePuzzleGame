using ScriptableObjectEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private SOGameEvent _sceneChangeRequest;
    [SerializeField] int toSceneIndex;
    [SerializeField] string toSceneName;
    [SerializeField] bool useSceneName;
    [SerializeField] bool goToNextScene;
    public void ChangeScene()
    {
        if(goToNextScene)
        {
            int currentIndex = SceneManager.GetSceneAt(1).buildIndex;
            _sceneChangeRequest.Raise(this, currentIndex + 1);
        }
        else
        {
            if(useSceneName) _sceneChangeRequest.Raise(this, toSceneName);
            else _sceneChangeRequest.Raise(this, toSceneIndex);
        }
    }
}
