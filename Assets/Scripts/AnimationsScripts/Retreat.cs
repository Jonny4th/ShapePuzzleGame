using System.Collections;
using UnityEngine;

public class Retreat : AnimatedElement
{
    [SerializeField] private AnimationCurve m_Motion;
    [SerializeField] private float m_AnimationTime;

    private bool m_IsMoving = false;

    public override void Perform()
    {
        if (m_IsMoving) return;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        m_IsMoving = true;

        var origin = transform.position;
        var startTime = Time.time;
        var endTime = startTime + m_AnimationTime;
        var normalizeTime = 0f;

        while (normalizeTime < 1)
        {
            var evaluate = m_Motion.Evaluate(normalizeTime);
            transform.position = origin + new Vector3(0, evaluate, 0);
            yield return null;
            normalizeTime = Mathf.InverseLerp(startTime, endTime, Time.time);
        }

        m_IsMoving = false;
        OnAnimationFinish();
    }
}
