using Scripts.Models;
using UnityEngine;

public class StageDataStorage : MonoBehaviour
{
    [SerializeField] private StageDataSO[] m_StageDataScriptables;
    public StageDataSO[] StageDataScriptables => m_StageDataScriptables;
}
