namespace Game.World
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    public class GlowHighlight : MonoBehaviour
    {
        private readonly Dictionary<Renderer, Material[]> _glowMaterials = new();
        private readonly Dictionary<Renderer, Material[]> _defaultMaterials = new();
        private readonly Dictionary<Color, Material> _cachedGlowMaterials = new();

        public Material glowMaterial;

        public bool isGlowing;

        private void Awake()
        {
            PrepareMaterials();
            
#if UNITY_EDITOR
            var components = GetComponents<GlowHighlight>();
            if (components.Length != 1)
            {
                Debug.LogError($"GameObject {name} has more than one {nameof(GlowHighlight)}");
            }
#endif
        }

        private void PrepareMaterials()
        {
            var childRenderers = GetComponentsInChildren<Renderer>()
                .ToArray();
            foreach (var childRenderer in childRenderers)
            {
                var originalMaterials = childRenderer.materials;
                _defaultMaterials.Add(childRenderer, originalMaterials);
                var newMaterials = new Material[originalMaterials.Length];
                for (var i = 0; i < originalMaterials.Length; i++)
                {
                    var originalMaterial = originalMaterials[i];
                    if (!_cachedGlowMaterials.TryGetValue(originalMaterial.color, out var material))
                    {
                        _cachedGlowMaterials[originalMaterial.color] = material = new Material(glowMaterial)
                        {
                            color = originalMaterial.color // This is the _color in the shader graph.
                        };
                    }

                    newMaterials[i] = material;
                }
                _glowMaterials.Add(childRenderer, newMaterials);
            }
        }

        public void ToggleGlow(bool state)
        {
            if (isGlowing == state)
            {
                return;
            }

            isGlowing = !state;
            ToggleGlow();
        }

        private void ToggleGlow()
        {
            if (isGlowing)
            {
                foreach (var kvp in _defaultMaterials)
                {
                    if (kvp.Key.gameObject.CompareTag(Tags.OnlyShowWhenGlowing))
                    {
                        kvp.Key.enabled = false;
                    }
                    kvp.Key.materials = kvp.Value;
                }
            }
            else
            {
                foreach (var kvp in _glowMaterials)
                {
                    kvp.Key.materials = kvp.Value;
                    if (kvp.Key.gameObject.CompareTag(Tags.OnlyShowWhenGlowing))
                    {
                        kvp.Key.enabled = true;
                    }
                }
            }

            isGlowing = !isGlowing;
        }
    }
}
