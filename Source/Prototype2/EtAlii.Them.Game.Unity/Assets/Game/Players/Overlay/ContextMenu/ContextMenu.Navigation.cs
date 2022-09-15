namespace Game.Players
{
    using System;
    using System.Linq;
    using UnityEngine.UIElements;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public partial class ContextMenu
    {
        private void OnNavigateContextMenu(InputAction.CallbackContext context)
        {
            if (!_canNavigate)
            {
                return;
            }
            _canNavigate = false;
            
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
            overlaySource.ContextMenu
                .DoRotate(menuEndRotation, 0.5f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => _canNavigate = true);
            
            // And rotate the icons, but in the opposite direction.
            var menuIcons = overlaySource.ContextMenu
                .Query("ContextMenuItem")
                .ToList()
                .Select(i => i.Query("Icon").First())
                .ToArray();
            foreach (var menuIcon in menuIcons)
            {
                var endRotation = menuIcon.style.rotate.value.angle.value - degrees;
                menuIcon
                    .DoRotate(endRotation, 0.25f)
                    .SetEase(Ease.Linear);
            }
        }
    }
}