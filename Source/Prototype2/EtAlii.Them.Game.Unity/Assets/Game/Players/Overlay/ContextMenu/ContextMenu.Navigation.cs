namespace Game.Players
{
    using System;
    using UnityEngine.UIElements;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public partial class ContextMenu
    {
        private int _rotation;

        private const float RotationDuration = 0.35f;
        private const float IconRotationDuration = 0.175f;
        
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
                _canNavigate = true;
            }
            else if (Math.Abs(direction.x - 1f) < Tolerance && direction.y == 0)
            {
                // Right
                _canNavigate = true;
            }
            else if (Math.Abs(direction.y + 1f) < Tolerance && direction.x == 0)
            {
                // Up
                Rotate(+1);
            }
            else if (Math.Abs(direction.y - 1f) < Tolerance && direction.x == 0)
            {
                // Down
                Rotate(-1);
            }
        }
        private void Rotate(int rotation)
        {
            var currentRotation = _rotation * 60 + 30;
            _rotation += rotation;
            var newRotation = _rotation * 60 + 30; 

            // Rotate the menu.
            overlaySource.ContextMenu
                .Query("Rotation")
                .First()
                .DoRotate(currentRotation, newRotation, RotationDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => _canNavigate = true);
            
            // And rotate the icons, but in the opposite direction.
            foreach (var item in items)
            {
                var iconElement = item.iconElement;
                currentRotation = -90 - item.rotation * 60;
                item.rotation += rotation;
                newRotation = -90 - item.rotation * 60;
                iconElement
                    .DoRotate(currentRotation, newRotation, IconRotationDuration)
                    .SetEase(Ease.Linear);
                item.isSelected = Math.Abs(item.rotation) % 6 == SelectionPosition;
                var color = item.isSelected ? Color.yellow : Color.white;
                iconElement
                    .DoColor(color, RotationDuration)
                    .SetEase(Ease.Linear);
            }
        }
    }
}