using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPlatformEnable : MonoBehaviour
{
    private RectTransform ui;

    void Awake()
    {
        ui = GetComponent<RectTransform>();
        EnableOnAndroidOnly();
    }

    public void EnableOnAndroidOnly()
    {
        ui.gameObject.SetActive(Application.platform == RuntimePlatform.Android);
    }
}
