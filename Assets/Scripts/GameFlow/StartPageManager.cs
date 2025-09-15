using ScriptableObjectEvent;
using UnityEngine;

public class StartPageManager : MonoBehaviour
{
    [SerializeField] private SOGameEvent _sceneChangeRequest;
    [SerializeField] private string HomeScene;

    public void StartGame()
    {
        _sceneChangeRequest.Raise(this, HomeScene);
    }
}
