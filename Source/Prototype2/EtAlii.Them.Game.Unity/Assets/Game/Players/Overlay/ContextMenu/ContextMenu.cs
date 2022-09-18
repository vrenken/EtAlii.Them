namespace Game.Players
{
    using System.Linq;
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;

    public partial class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public InputActionsSource inputActionsSource;

        public ContextMenuItem[] items;
        private ContextMenuItem[] _activeItems;
        private const double Tolerance = 0.00001f;

        private bool _canNavigate = true;

        private void OnEnable()
        {
            inputActionsSource.InputActions.Player.ShowContextMenu.started += OnShowContextMenu;
            inputActionsSource.InputActions.ContextMenu.HideContextMenu.canceled += OnHideContextMenu;
            inputActionsSource.InputActions.ContextMenu.Navigate.performed += OnNavigateContextMenu;
            inputActionsSource.InputActions.ContextMenu.Build.performed += OnBuild;
        }

        private void OnDisable()
        {
            inputActionsSource.InputActions.Player.ShowContextMenu.started -= OnShowContextMenu;
            inputActionsSource.InputActions.ContextMenu.HideContextMenu.canceled -= OnHideContextMenu;
            inputActionsSource.InputActions.ContextMenu.Navigate.performed -= OnNavigateContextMenu;
            inputActionsSource.InputActions.ContextMenu.Build.performed -= OnBuild;
        }

        public void Prepare(HexTile tile, out bool canBeUsed)
        {
            var grid = hexTileSelector.hexGrid;

            _activeItems = items
                .Where(i => i.action != null && i.action.IsValid(grid, tile, out var _, out var _))
                .ToArray();
            
            canBeUsed = _activeItems.Any();
        }

        private void OnBuild(InputAction.CallbackContext obj)
        {
            var tile = hexTileSelector.hexTile;
            if (tile == null) return;

            var action = _activeItems
                .Where(i => i.isSelected)
                .Select(i => i.action)
                .SingleOrDefault();
            if (action != null)
            {
                var grid = hexTileSelector.hexGrid;
                if (action.IsValid(grid, tile, out var _, out var preparations))
                {
                    action.Invoke(grid, tile, preparations);
                }
            }

            OnHideContextMenu(obj);
        }
    }
}