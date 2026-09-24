using Scripts.Walls;
using UnityEngine;
using UnityEngine.Events;

public class ClueTile : TileableMono
{
    [SerializeField] private TileIdentifier _identifier;
    [SerializeField] private TileStateVisualController _panelStateVisual;
    [SerializeField] private BlockDetector _blockDetector;
    [SerializeField] private State _currentState;

    public State CurrentState { get { return _currentState; } }

    public UnityEvent<State> OnClueStateChanged;

    public override TileIdentifier Identifier
    {
        get => _identifier;
        set => _identifier = value;
    } 

    public enum State
    {
        None = 0b_0000_0000,
        Target = 0b_0000_0001,
        Shadow = 0b_0000_0010,
        Correct = Target | Shadow,
    }

    private void OnEnable()
    {
        _blockDetector.OnBlockEnter.AddListener(HandleOnBlockEnter);
        _blockDetector.OnBlockExit.AddListener(HandleOnBlockExit);
    }

    private void OnDisable()
    {
        _blockDetector.OnBlockEnter.RemoveListener(HandleOnBlockEnter);
        _blockDetector.OnBlockExit.RemoveListener(HandleOnBlockExit);
    }

    private void HandleOnBlockEnter()
    {
        var state = _currentState | State.Shadow;
        ChangeStateTo(state);
    }

    private void HandleOnBlockExit()
    {
        var state = _currentState & ~State.Shadow;
        ChangeStateTo(state);
    }

    public void ChangeStateTo(State state)
    {
        _currentState = state;
        _panelStateVisual.UpdateVisual(_currentState);
    }

    public void SetAsClue(bool v)
    {
        var state = State.None;

        if(v)
        {
            state = _currentState | State.Target;
        }
        else
        {
            state = _currentState & ~State.Target;
        }

        ChangeStateTo(state);
    }
}