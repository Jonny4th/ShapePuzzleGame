using UnityEditor.Presets;
using UnityEngine;

namespace Utilities
{
    [CreateAssetMenu(fileName = "newPresetPlatform", menuName = "Preset Platform")]
    public class PresetPlaform : ScriptableObject
    {
        public Preset Preset;
        public RuntimePlatform Platform;
    }
}
