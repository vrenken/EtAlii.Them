namespace Game.Players
{
    using System.Linq;
    using UnityEngine.InputSystem;
    using UnityEngine;

    public partial class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public InputActionsSource inputActionsSource;

        public ContextMenuItem[] items;
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


        private void OnBuild(InputAction.CallbackContext obj)
        {
            var tile = hexTileSelector.hexTile;
            if (tile == null) return;

            var action = items
                .Where(i => i.isSelected)
                .Select(i => i.action)
                .SingleOrDefault();
            if (action != null)
            {
                if (action.IsValid(tile, out var _))
                {
                    action.Invoke(tile, hexTileSelector.hexGrid);
                }
            }

            OnHideContextMenu(obj);
        }
    }
}