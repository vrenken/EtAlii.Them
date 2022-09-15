namespace Game.Players
{
    using System;
    using UnityEngine;
    using UnityEngine.UIElements;

    public partial class ContextMenu
    {
        private const int SelectionPosition = 3;
        private void SetMenuItems()
        {
            var menuItems = overlaySource.ContextMenu
                .Query("ContextMenuItem")
                .ToList()
                .ToArray();
            for (var i = 0; i < menuItems.Length; i++)
            {
                var iconElement = menuItems[i].Query("Icon").First();
                items[i].iconElement = iconElement;
                SetIcon(i);
            }
        }

        private void SetIcon(int index)
        {
            var item = items[index];
            item.rotation = _rotation + index;
            var currentRotation = -90 - item.rotation * 60;
            item.iconElement.style.backgroundImage = new StyleBackground(item.icon);
            item.iconElement.style.backgroundColor = new StyleColor(); // Let's clear the development color.
            item.iconElement.style.rotate = new StyleRotate(new Rotate(currentRotation));
            item.isSelected = Math.Abs(item.rotation) % 6 % 6 == SelectionPosition;
            var color = item.isSelected ? Color.yellow : Color.white;
            item.iconElement.style.unityBackgroundImageTintColor = new StyleColor(color);
        }
        private void SetPosition()
        {
            // We want to focus on the upper face of the tile. This means adding an offset to aim for.
            var worldPosition = hexTileSelector.hexTile.transform.position + Vector3.up;
            var position = RuntimePanelUtils.CameraTransformWorldToPanel(overlaySource.Screen.panel, worldPosition, playerCamera);
            overlaySource.ContextMenu.style.left = position.x - overlaySource.ContextMenuSize.x / 2f;
            overlaySource.ContextMenu.style.top = position.y - overlaySource.ContextMenuSize.y / 2f;
        }
    }
}