using Scripts.Walls;
using UnityEngine;

public class PanelEntity : TileableMono
{
    [SerializeField] private PanelIdentifier _identifier;
    [SerializeField] private PanelStateController _panelState;

    public override PanelIdentifier Identifier
    {
        get => _identifier;
        set => _identifier = value;
    } 

    public PanelStateController PanelState => _panelState;
}