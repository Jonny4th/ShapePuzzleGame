using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Stage Data")]
public class StageData : ScriptableObject
{
    public string StageName;
    public PanelOnOff[] ActivePanels;
}
