using System.Collections;
using UnityEngine;

public class RotationCue : AnimatedElement
{
    [SerializeField] private Transform m_Rotatee;
    [SerializeField] private Quaternion m_TargatOrientation;
    [SerializeField] private float m_MaximumDegreeRotate; // per second.
    
    private bool m_IsRotating = false;

    public override void Perform()
    {
        if (m_IsRotating) return;
        
        if (TryGetComponent<Animation>(out var animation))
        {
            animation.enabled = false;
        }

        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        m_IsRotating = true;
        float degreePerFrame = m_MaximumDegreeRotate * Time.deltaTime; // degree per sec * sec per frame = degree per frame.
        
        while (m_Rotatee.rotation != m_TargatOrientation)
        {
            m_Rotatee.rotation = Quaternion.RotateTowards(m_Rotatee.rotation, m_TargatOrientation, degreePerFrame);
            yield return null;
        }

        OnAnimationFinish();
        m_IsRotating = false;
    }
}
