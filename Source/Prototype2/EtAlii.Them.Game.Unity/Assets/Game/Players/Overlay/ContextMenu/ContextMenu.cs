namespace Game.Players
{
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;
    using UnityEngine.UIElements;

    public partial class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public HexTile tilePrefab;

        public InputActionsSource inputActionsSource;

        public VectorImage[] icons;
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

        private void SetMenuItems()
        {
            var menuItems = overlaySource.ContextMenu
                .Query("ContextMenuItem")
                .ToList()
                .ToArray();
            SetIcon(0, menuItems, -60);
            SetIcon(1, menuItems, -120);
            SetIcon(2, menuItems, -180);
            SetIcon(3, menuItems, -240);
        }

        private void SetIcon(int index, VisualElement[] menuItems, float rotation)
        {
            var iconElement = menuItems[index].Query("Icon").First();
            var icon = icons[index];
            iconElement.style.backgroundImage = new StyleBackground(icon);
            iconElement.style.backgroundColor = new StyleColor(); // Let's clear the development color.
            
            iconElement.style.rotate = new StyleRotate(new Rotate(rotation));
        }
        private void SetPosition()
        {
            // We want to focus on the upper face of the tile. This means adding an offset to aim for.
            var worldPosition = hexTileSelector.hexTile.transform.position + Vector3.up;
            var position = RuntimePanelUtils.CameraTransformWorldToPanel(overlaySource.Screen.panel, worldPosition, playerCamera);
            overlaySource.ContextMenu.style.left = position.x - overlaySource.ContextMenuSize.x / 2f;
            overlaySource.ContextMenu.style.top = position.y - overlaySource.ContextMenuSize.y / 2f;
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