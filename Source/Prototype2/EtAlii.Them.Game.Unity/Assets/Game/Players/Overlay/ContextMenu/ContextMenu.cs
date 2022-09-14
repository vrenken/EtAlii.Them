namespace Game.Players
{
    using System;
    using System.Linq;
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;
    using UnityEngine.UIElements;
    using DG.Tweening;

    public class ContextMenu : MonoBehaviour
    {
        public Camera playerCamera;
        public HexTileSelector hexTileSelector;
        public OverlaySource overlaySource;

        public HexTile tilePrefab;

        public InputActionsSource inputActionsSource;

        public VectorImage[] icons;
        private const double Tolerance = 0.00001f;

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

        private void OnNavigateContextMenu(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            if (Math.Abs(direction.x + 1f) < Tolerance && direction.y == 0)
            {
                // Left
            }
            else if (Math.Abs(direction.x - 1f) < Tolerance && direction.y == 0)
            {
                // Right
            }
            else if (Math.Abs(direction.y + 1f) < Tolerance && direction.x == 0)
            {
                // Up
                Rotate(60f);
            }
            else if (Math.Abs(direction.y - 1f) < Tolerance && direction.x == 0)
            {
                // Down
                Rotate(-60f);
            }
        }

        private void Rotate(float degrees)
        {
            // Rotate the menu.
            var menuEndRotation = overlaySource.ContextMenu.style.rotate.value.angle.value + degrees;
            DOTween
                .To(() => overlaySource.ContextMenu.style.rotate.value.angle.value, rotation => overlaySource.ContextMenu.style.rotate = new StyleRotate(new Rotate(new Angle(rotation, AngleUnit.Degree))), menuEndRotation, 0.5f)
                .SetEase(Ease.OutCubic);
            
            // And rotate the icons, but in the opposite direction.
            var menuIcons = overlaySource.ContextMenu
                .Query("ContextMenuItem")
                .ToList()
                .Select(i => i.Query("Icon").First())
                .ToArray();
            foreach (var menuIcon in menuIcons)
            {
                var endRotation = menuIcon.style.rotate.value.angle.value - degrees;
                DOTween
                    .To(() => menuIcon.style.rotate.value.angle.value, rotation => menuIcon.style.rotate = new StyleRotate(new Rotate(new Angle(rotation, AngleUnit.Degree))), endRotation, 0.5f)
                    .SetEase(Ease.Linear);
            }

        }

        private void OnShowContextMenu(InputAction.CallbackContext context)
        {
            if (hexTileSelector.hexTile == null) return;

            overlaySource.ContextMenu.style.display = DisplayStyle.Flex;

            SetMenuItems();
            SetPosition();
            
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