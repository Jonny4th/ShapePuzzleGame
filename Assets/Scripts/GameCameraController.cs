using DG.Tweening;
using Scripts.Models;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCameraController : MonoBehaviour
{
    [SerializeField] private float m_MaximumDeviatedAngle;

    [SerializeField] private GameObject m_Subject;

    private Vector3 m_InitialAngle;
    private Vector3 m_TargetAngle;

    void Awake()
    {
        m_InitialAngle = m_Subject.transform.eulerAngles;
    }

    public void OnLevelConstruct(LevelBlueprint blueprint)
    {
        Vector3 levelSize = blueprint.Data.StageSize;
        levelSize = Vector3.Scale(levelSize, new Vector3(-1, 1, -1));
        SetPosition(levelSize / 2);
    }

    public void RotateViewAngle(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
        RotateOnYAxisNormalized(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">0 to 1 -> default to max angle</param>
    public void RotateOnYAxisNormalized(float value)
    {
        var angle = Mathf.LerpUnclamped(0, m_MaximumDeviatedAngle, value);
        RotateOnYAxis(angle);
    }

    public void RotateOnYAxis(float angle)
    {
        m_TargetAngle = m_InitialAngle + Vector3.down*angle;
        m_Subject.transform.DORotate(m_TargetAngle, 1f);
    }

    public void SetPosition(Vector3 position)
    {
        m_Subject.transform.position = position;
    }
}
