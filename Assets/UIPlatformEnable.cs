using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPlatformEnable : MonoBehaviour
{
    [SerializeField] private RuntimePlatform enablePlatform;
    [SerializeField] private bool m_Perform = true;

    void Awake()
    {
        if (!m_Perform) return;
        CheckEnable();
    }

    public void CheckEnable()
    {
        gameObject.SetActive(Application.platform == enablePlatform);
    }
}