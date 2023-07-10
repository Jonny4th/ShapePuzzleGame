using UnityEngine;

namespace Utilities
{
    [CreateAssetMenu(fileName = "newPlatformGameObject", menuName = "Platform Game Object")]
    public class PlaformGameObject : ScriptableObject
    {
        public GameObject GameObject;
        public RuntimePlatform Platform;
    }
}
