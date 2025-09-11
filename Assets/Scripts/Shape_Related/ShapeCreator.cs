using UnityEngine;

public class ShapeCreator : MonoBehaviour
{
    [SerializeField]
    GameObject m_Origin;

    [SerializeField]
    Transform m_Parent;

    [SerializeField]
    Vector3Int[] m_Configuration;

    public void Create()
    {
        if(m_Parent == null) m_Parent = transform;

        foreach(var pos in m_Configuration)
        {
            Instantiate(m_Origin, pos, Quaternion.identity, m_Parent);
        }
    }
}
