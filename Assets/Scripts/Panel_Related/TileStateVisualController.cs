using UnityEngine;
using State = ClueTile.State;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

[RequireComponent(typeof(MeshRenderer))]
public class TileStateVisualController : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_Renderer; 
    
    [Header("Visuals")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material targetMaterial;
    [SerializeField] Material shadowMaterial;
    [SerializeField] Material correctMaterial;

    public void UpdateVisual(State state)
    {
        switch(state)
        {
            case State.None: m_Renderer.material = defaultMaterial; break;
            case State.Target: m_Renderer.material = targetMaterial; break;
            case State.Shadow: m_Renderer.material = shadowMaterial; break;
            case State.Correct: m_Renderer.material = correctMaterial; break;
        }
    }
}
