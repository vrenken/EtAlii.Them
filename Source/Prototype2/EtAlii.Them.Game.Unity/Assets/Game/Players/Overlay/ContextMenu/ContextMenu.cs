namespace Game.Players
{
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;

    public partial class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public HexTile tilePrefab;

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
            if (hexTileSelector.hexTile == null) return;
            
            var coordinates = hexTileSelector.hexTile.HexCoordinates;

            hexTileSelector.hexGrid.ReplaceTile(coordinates, tilePrefab);

            OnHideContextMenu(obj);
        }
    }
}