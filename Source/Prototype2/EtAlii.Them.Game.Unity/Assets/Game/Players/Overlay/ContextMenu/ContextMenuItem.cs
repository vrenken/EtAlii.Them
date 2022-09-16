namespace Game.Players
{
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>
    /// Left = 0
    /// Left top = 1
    /// Right top = 2
    /// Right = 3
    /// Right bottom = 4
    /// Left bottom = 5
    /// </summary>
    [CreateAssetMenu(menuName = "Them/ContextMenu/Item")]
    public class ContextMenuItem : ScriptableObject
    {
        public VectorImage icon;
        public int rotation;
        public string title;
        public bool isSelected;
        public ContextMenuItem[] children;
        public VisualElement iconElement;

        public MenuItemAction action;
    }
}