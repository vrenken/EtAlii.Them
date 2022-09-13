namespace Game.Players
{
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public HexTile tilePrefab;

        public InputActionsSource inputActionsSource;

        public VectorImage[] icons;
        
        private void OnEnable()
        {
            inputActionsSource.InputActions.Player.ShowContextMenu.performed += OnShowContextMenu;
            inputActionsSource.InputActions.ContextMenu.HideContextMenu.performed += OnHideContextMenu;
            inputActionsSource.InputActions.ContextMenu.Build.performed += OnBuild;
        }

        private void OnDisable()
        {
            inputActionsSource.InputActions.Player.ShowContextMenu.performed -= OnShowContextMenu;
            inputActionsSource.InputActions.ContextMenu.HideContextMenu.performed -= OnHideContextMenu;
            inputActionsSource.InputActions.ContextMenu.Build.performed -= OnBuild;
        }

        private void OnShowContextMenu(InputAction.CallbackContext obj)
        {
            if (hexTileSelector.hexTile == null) return;

            overlaySource.ContextMenu.style.display = DisplayStyle.Flex;

            SetPosition();
            SetMenuItems();
            
            inputActionsSource.InputActions.Player.Disable();
            inputActionsSource.InputActions.ContextMenu.Enable();
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
            iconElement.style.rotate = new StyleRotate(new Rotate(rotation));
        }
        private void SetPosition()
        {
            
            var position = RuntimePanelUtils.CameraTransformWorldToPanel(overlaySource.Screen.panel, hexTileSelector.hexTile.transform.position, playerCamera);

            //overlaySource.ContextMenu.transform.position = position;
            var layout = overlaySource.ContextMenu.layout;
            //overlaySource.ContextMenu.transform.position = position.WithNewX(position.x - layout.width / 2);

            //overlaySource.ContextMenu.transform.position = position
            //    .WithNewX(position.x - layout.size.x / 2);
            
            //
            // var position = overlaySource.Screen.WorldToLocal(hexTileSelector.hexTile.transform.position);
            // overlaySource.ContextMenu.style.left = position.x - overlaySource.ContextMenu.layout.width / 2f;
            // overlaySource.ContextMenu.style.top = position.y - overlaySource.ContextMenu.layout.height / 2f;
            //
            // m_Bar.transform.position = newPosition.WithNewX(newPosition.x - 
            //                                                 m_Bar.layout.width / 2);
        }
        
        private void OnHideContextMenu(InputAction.CallbackContext obj)
        {
            inputActionsSource.InputActions.ContextMenu.Disable();
            inputActionsSource.InputActions.Player.Enable();
            overlaySource.ContextMenu.style.display = DisplayStyle.None;
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