using ScriptableObjectEvent;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageManager : MonoBehaviour
{
    [SerializeField] private SOGameEvent _sceneChangeRequest;
    [SerializeField] private string HomeScene;

    public void StartGame()
    {
        _sceneChangeRequest.Raise(this, HomeScene);
    }
}
