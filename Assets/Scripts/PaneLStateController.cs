using UnityEngine;
using UnityEngine.InputSystem.HID;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

public class PaneLStateController : MonoBehaviour
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
    [SerializeField] State currentState;

    [Header("Visuals")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material targetMaterial;
    [SerializeField] Material shadowMaterial;
    [SerializeField] Material correctMaterial;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        UpdateState();
    }
#endif

    void UpdateState()
    {
        if (Physics.Raycast(transform.position,transform.up, 10f))
        {
            Debug.DrawRay(transform.position, transform.up * 10f, Color.yellow);
            currentState |= State.Shadow;
        }
        else
        {
            currentState &= ~State.Shadow;
        }
        UpdateVisual();
    }

    void UpdateVisual()
    {
        switch (currentState)
        {
            case State.None: mesh.material = defaultMaterial; break;
            case State.Target: mesh.material = targetMaterial; break;
            case State.Shadow: mesh.material = shadowMaterial; break;
            case State.Correct: mesh.material = correctMaterial; break;
        }
    }


}
