using System.Collections;
using UnityEngine;

public class RotationCue : AnimatedElement
{
    [SerializeField] private Transform m_Rotatee;
    [SerializeField] private Quaternion m_TargatOrientation;
    [SerializeField] private float m_MaximumDegreeRotate;
    
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
        
        while (m_Rotatee.rotation != m_TargatOrientation)
        {
            m_Rotatee.rotation = Quaternion.RotateTowards(m_Rotatee.rotation, m_TargatOrientation, m_MaximumDegreeRotate);
            yield return null;
        }

        OnAnimationFinish();
        m_IsRotating = false;
    }
}
