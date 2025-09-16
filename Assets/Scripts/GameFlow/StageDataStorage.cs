using Scripts.Models;
using UnityEngine;

public class StageDataStorage : MonoBehaviour
{
    [SerializeField] private StageDataCollection m_StageDataCollection;
    public StageDataCollection StageDataCollection => m_StageDataCollection;
}
