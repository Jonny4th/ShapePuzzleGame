using UnityEngine;
using State = ClueTile.State;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

[RequireComponent(typeof(MeshRenderer))]
public class TileStateVisualController : MonoBehaviour
{
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

    public void UpdateVisual(State state)
    {
        switch(state)
        {
            case State.None: mesh.material = defaultMaterial; break;
            case State.Target: mesh.material = targetMaterial; break;
            case State.Shadow: mesh.material = shadowMaterial; break;
            case State.Correct: mesh.material = correctMaterial; break;
        }
    }
}
