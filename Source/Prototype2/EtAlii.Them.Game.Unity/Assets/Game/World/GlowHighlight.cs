namespace Game.World
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    public class GlowHighlight : MonoBehaviour
    {
        private readonly Dictionary<Renderer, Material[]> _glowMaterials = new();
        private readonly Dictionary<Renderer, Material[]> _defaultMaterials = new();
        private readonly Dictionary<Color, Material> _cachedGlowMaterials = new();

        public Material glowMaterial;

        public bool isInvisible;
        public bool isGlowing;

        private MeshRenderer _meshRenderer;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            
            PrepareMaterials();
        }

        private void PrepareMaterials()
        {
            foreach (var childRenderer in GetComponentsInChildren<Renderer>())
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
                if (isInvisible)
                {
                    _meshRenderer.enabled = true;
                }
                foreach (var kvp in _defaultMaterials)
                {
                    kvp.Key.materials = kvp.Value;
                }
            }
            else
            {
                foreach (var kvp in _glowMaterials)
                {
                    kvp.Key.materials = kvp.Value;
                }
                if (isInvisible)
                {
                    _meshRenderer.enabled = false;
                }
            }

            isGlowing = !isGlowing;
        }
    }
}
