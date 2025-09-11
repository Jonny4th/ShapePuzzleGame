using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

[RequireComponent(typeof(MeshRenderer))]
public class PanelStateController : MonoBehaviour
{
    [SerializeField] LayerMask hitLayer;
    int hitLayerMask { get { return 1 << hitLayer.value; } }
    public enum State
    {
        None = 0b_0000_0000,
        Target = 0b_0000_0001,
        Shadow = 0b_0000_0010,
        Correct = Target | Shadow,
    }
    public State currentState;

    [Header("Visuals")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material targetMaterial;
    [SerializeField] Material shadowMaterial;
    [SerializeField] Material correctMaterial;
    MeshRenderer mesh;

    public void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void LateUpdate()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if(Physics.Raycast(transform.position, -transform.forward, 10f, ~hitLayerMask))
        {
            currentState |= State.Shadow;
        }
        else
        {
            currentState &= ~State.Shadow;
        }

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        switch(currentState)
        {
            case State.None: mesh.material = defaultMaterial; break;
            case State.Target: mesh.material = targetMaterial; break;
            case State.Shadow: mesh.material = shadowMaterial; break;
            case State.Correct: mesh.material = correctMaterial; break;
        }
    }

    public void SetAsTarget(bool v)
    {
        if(v)
        {
            currentState |= State.Target;
        }
        else
        {
            currentState &= ~State.Target;
        }
    }
}
