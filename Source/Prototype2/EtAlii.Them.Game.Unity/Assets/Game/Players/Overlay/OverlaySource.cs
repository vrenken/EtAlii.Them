namespace Game.Players
{
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class OverlaySource : MonoBehaviour
    {
        public UIDocument overlay;

        public VisualElement ContextMenu { get; private set; }

        public VisualElement Screen { get; private set; }
        private void Awake()
        {
            Screen = overlay.rootVisualElement.Q("Screen");
            ContextMenu = overlay.rootVisualElement.Q("ContextMenu");
            ContextMenu.style.display = DisplayStyle.None;
        }
    }
}
