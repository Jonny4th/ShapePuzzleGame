using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    [SerializeField] CanvasRenderer previous;
    
    public void Back()
    {
        previous.gameObject.SetActive(true);
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
