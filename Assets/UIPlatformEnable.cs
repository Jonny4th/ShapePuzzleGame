using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIPlatformEnable : MonoBehaviour
{
    [SerializeField] private PlatformPair[] m_Entries;

    void Awake()
    {
        CheckEnable();
    }

    public void CheckEnable()
    {
        var currentPlatform = Application.platform;

        foreach(var component in m_Entries)
        {
            if(component.Platform.Any(triggerPlatform => triggerPlatform == currentPlatform)) component.Component.SetActive(true); 
            else component.Component.SetActive(false);
        }
    }
}

[Serializable]
internal struct PlatformPair
{
    public GameObject Component;
    public RuntimePlatform[] Platform;
}