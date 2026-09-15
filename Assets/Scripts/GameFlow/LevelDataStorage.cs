using Scripts.Models;
using UnityEngine;

public class LevelDataStorage : MonoBehaviour
{
    [SerializeField] private LevelDataCollection m_StageDataCollection;
    public LevelDataCollection StageDataCollection => m_StageDataCollection;
}
