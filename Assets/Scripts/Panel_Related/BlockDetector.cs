using UnityEngine;
using UnityEngine.Events;

public class BlockDetector : MonoBehaviour
{
    [SerializeField] LayerMask hitLayer;
    int hitLayerMask { get { return 1 << hitLayer.value; } }

    public UnityEvent OnBlockEnter;
    public UnityEvent OnBlockExit;

    public void LateUpdate()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if(Physics.Raycast(transform.position, -transform.forward, 10f, ~hitLayerMask))
        {
            OnBlockEnter?.Invoke();
        }
        else
        {
            OnBlockExit?.Invoke();
        }
    }
}