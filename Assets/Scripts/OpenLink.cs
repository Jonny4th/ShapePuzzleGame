using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenLink : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Camera canvasCamera;
    [SerializeField] string link;
    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) canvasCamera = null;
        else canvasCamera = GetComponentInParent<Canvas>().worldCamera;
    }
    private void OnMouseOver() {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, canvasCamera);
        Debug.Log(linkIndex);
    }
    private void OnMouseDown() {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, canvasCamera);
        Debug.Log(linkIndex);
        OpenURL(link);
    }
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
