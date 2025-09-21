using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AnimatedElementController : MonoBehaviour
{
    [SerializeField] private AnimatedElement[] m_Elements;
    [SerializeField] private float m_Delay;

    private bool m_IsRunning = false;
    private int m_ElementsCount;

    public UnityEvent OnAnimationFinish;

    void Awake()
    {
        m_ElementsCount = m_Elements.Length;

        foreach (var element in m_Elements)
        {
            element.FinishAnimaiton += CountAnimationFinished;
        }
    }

    private void CountAnimationFinished(AnimatedElement element)
    {
        m_ElementsCount--;
    }

    public void Perform()
    {
        if (m_IsRunning) return;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        m_IsRunning = true;

        foreach (var e in m_Elements)
        {
            e.Perform();
            yield return new WaitForSecondsRealtime(m_Delay);
        }

        yield return new WaitUntil(() => m_ElementsCount == 0);
        m_IsRunning = false;
        Debug.Log("All animation played.");
        OnAnimationFinish.Invoke();
    }
}

public abstract class AnimatedElement : MonoBehaviour
{
    public event Action<AnimatedElement> FinishAnimaiton;

    public abstract void Perform();

    protected virtual void OnAnimationFinish()
    {
        FinishAnimaiton?.Invoke(this);
    }
}