using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePersistantManager : MonoBehaviour
{
    [SerializeField] private string _startScene;

    private Scene _currentScene;

    void Start()
    {
        SceneManager.LoadScene(_startScene, LoadSceneMode.Additive);
        _currentScene = SceneManager.GetSceneAt(1);
    }

    public void HandleSceneChangeRequest(Component _, object arg)
    {
        if(arg == null) throw new System.ArgumentException($"Arg is null.");

        if (arg is string newScene)
        {
            StartCoroutine(SwitchScene(newScene));
            return;
        }
        else if (arg is int newIndex)
        {
            StartCoroutine(SwitchScene(newIndex));
            return;
        }

        throw new System.ArgumentException($"Must be string for int.");
    }

    IEnumerator SwitchScene(string newScene)
    {
        DoBeforeUnloadScene();

        var process = SceneManager.UnloadSceneAsync(_currentScene);
        while (!process.isDone)
        {
            yield return null;
        }

        process = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!process.isDone)
        {
            yield return null;
        }

        _currentScene = SceneManager.GetSceneAt(1);

        DoAfterSceneLoaded();
    }

    IEnumerator SwitchScene(int newIndex)
    {
        DoBeforeUnloadScene();

        var process = SceneManager.UnloadSceneAsync(_currentScene);
        while (!process.isDone)
        {
            yield return null;
        }

        process = SceneManager.LoadSceneAsync(newIndex, LoadSceneMode.Additive);
        while (!process.isDone)
        {
            yield return null;
        }

        _currentScene = SceneManager.GetSceneAt(1);

        DoAfterSceneLoaded();
    }

    private void DoBeforeUnloadScene()
    {
        Debug.Log($"Closing {_currentScene.name} scene.");
    }

    private void DoAfterSceneLoaded()
    {
        Debug.Log($"{_currentScene.name} scene loaded.");
    }
}
