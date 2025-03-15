using System.Collections;
using UnityEngine;

public class ControllerPanelManager : MonoBehaviour
{
    [SerializeField]
    Transform m_SixDirectionPad;

    [SerializeField]
    private float m_RotationSpeed = 1f;
    
    Coroutine m_RotationProcess;

    private Quaternion m_OriginalOrientation;

    void Awake()
    {
        m_OriginalOrientation = m_SixDirectionPad.rotation;
    }

    public void OnRotationToggleChanged(bool isOn)
    {
        if(isOn)
        {
            CheckRotationCoroutine();

            m_RotationProcess = StartCoroutine(RotateTo(m_SixDirectionPad.rotation * Quaternion.Euler(0, 0, 90)));
        }
        else
        {
            CheckRotationCoroutine();

            m_RotationProcess = StartCoroutine(RotateTo(m_OriginalOrientation));
        }

        void CheckRotationCoroutine()
        {
            if(m_RotationProcess != null)
            {
                StopCoroutine(m_RotationProcess);
                m_RotationProcess = null;
            }
        }
    }

    IEnumerator RotateTo(Quaternion oriantation)
    {
        var newRotation = Quaternion.RotateTowards(m_SixDirectionPad.rotation, oriantation, m_RotationSpeed);

        while(m_SixDirectionPad.rotation != newRotation)
        {
            m_SixDirectionPad.rotation = newRotation;
            newRotation = Quaternion.RotateTowards(m_SixDirectionPad.rotation, oriantation, m_RotationSpeed);
            yield return null;
        }
    }
}
