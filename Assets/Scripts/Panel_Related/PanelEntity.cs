using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEntity : MonoBehaviour
{
    [SerializeField] private Vector3Int _relativeCoordinate;
    [SerializeField] private PanelStateController _panelState;

    public Vector3Int RelativeCoordinate => _relativeCoordinate;
    public PanelStateController PanelState => _panelState;
}
