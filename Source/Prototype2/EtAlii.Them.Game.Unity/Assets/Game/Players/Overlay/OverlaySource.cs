namespace Game.Players
{
    using UnityEngine;
    using UnityEngine.UIElements;

    public class OverlaySource : MonoBehaviour
    {
        public UIDocument overlay;

        //public Header Header { get; private set; }
        public VisualElement ContextMenu { get; private set; }
        public Vector2 ContextMenuSize { get; private set; }

        public VisualElement Screen { get; private set; }
        private void Awake()
        {
            Screen = overlay.rootVisualElement.Q("Screen");
            ContextMenu = overlay.rootVisualElement.Q("ContextMenu");
            ContextMenu.RegisterCallback<GeometryChangedEvent>(AcquireContextMenuSize);
        }

        private void AcquireContextMenuSize(GeometryChangedEvent evt)
        {
            ContextMenu.UnregisterCallback<GeometryChangedEvent>(AcquireContextMenuSize);

            ContextMenu.style.display = DisplayStyle.None;
            ContextMenuSize = new Vector2(ContextMenu.layout.width, ContextMenu.layout.height);
        }
    }
}
