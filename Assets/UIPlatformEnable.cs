using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPlatformEnable : MonoBehaviour
{
    [SerializeField]
    RuntimePlatform enablePlatform;

    void Awake()
    {
        CheckEnable();
    }

    public void CheckEnable()
    {
        gameObject.SetActive(Application.platform == enablePlatform);
    }
}