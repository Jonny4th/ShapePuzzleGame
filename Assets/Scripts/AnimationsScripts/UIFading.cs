using System.Collections;
using UnityEngine;

public class UIFading : AnimatedElement
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private AnimationCurve m_FadeFunction;
    [SerializeField] private float m_Duration;

    private bool m_IsRunning;

    public override void Perform()
    {
        if (m_IsRunning) return;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        m_IsRunning = true;
        m_CanvasGroup.interactable = false;

        var startTime = Time.time;
        var endTime = startTime + m_Duration;
        var normalizeTime = 0f;
        float originalAlpha = m_CanvasGroup.alpha;

        while (normalizeTime < 1)
        {
            m_CanvasGroup.alpha = originalAlpha * m_FadeFunction.Evaluate(normalizeTime);
            yield return null;
            normalizeTime = Mathf.InverseLerp(startTime, endTime, Time.time);
        }

        m_CanvasGroup.alpha = originalAlpha * m_FadeFunction.Evaluate(1);

        OnAnimationFinish();
        m_IsRunning = false;
    }
}
