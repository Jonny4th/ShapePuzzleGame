using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class PlatformGameObjectController : MonoBehaviour
    {
        [SerializeField]
        GameObject defaultGameObject;

        [SerializeField]
        List<PlaformGameObject> PlatformPresets;

        void Awake()
        {
            var go = PlatformPresets.Find(x => x.Platform == Application.platform);
            if(go != null)
            {
                var gameObject = Instantiate(go.GameObject, transform);

                if(defaultGameObject != null) Destroy(defaultGameObject);
            }
        }
    }
}

