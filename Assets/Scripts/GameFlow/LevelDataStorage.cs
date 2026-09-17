using Scripts.Models;
using UnityEngine;

public class LevelDataStorage : MonoBehaviour
{
    public static LevelDataStorage Instance {get; private set;} = null;

    void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    [SerializeField] private LevelDataCollection m_StageDataCollection;
    public LevelDataCollection StageDataCollection => m_StageDataCollection;
}
