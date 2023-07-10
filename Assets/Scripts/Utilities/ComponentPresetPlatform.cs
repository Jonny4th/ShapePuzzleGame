using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

namespace Utilities
{
    public class ComponentPresetPlatform : MonoBehaviour
    {
        [SerializeField]
        Component component;

        [SerializeField]
        Preset defaultPreset;

        [SerializeField]
        List<PresetPlaform> PlatformPresets;

        void Awake()
        {
            var preset = PlatformPresets.Find(x => x.Platform == Application.platform);
            if(preset != null)
            {
                preset.Preset.ApplyTo(component);
            }
            else
            {
                defaultPreset.ApplyTo(component);
            }
        }
    }
}

