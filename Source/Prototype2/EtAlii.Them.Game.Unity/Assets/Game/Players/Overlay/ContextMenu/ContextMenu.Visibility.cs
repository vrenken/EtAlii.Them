namespace Game.Players
{
    using UnityEngine.InputSystem;
    using UnityEngine;
    using UnityEngine.UIElements;
    using DG.Tweening;

    public partial class ContextMenu
    {
        private void OnShowContextMenu(InputAction.CallbackContext context)
        {
            if (hexTileSelector.hexTile == null) return;
            
            SetMenuItems();
            SetPosition();

            inputActionsSource.InputActions.Player.Disable();

            overlaySource.ContextMenu.style.display = DisplayStyle.Flex;

            _canNavigate = false;
            var startScale = 0.1f;
            var endScale = 1.0f;
            overlaySource.ContextMenu.style.scale = new StyleScale(new Scale(Vector3.one * startScale));
            overlaySource.ContextMenu
                .DOScale(endScale, 0.25f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    inputActionsSource.InputActions.ContextMenu.Enable();
                    _canNavigate = true;
                });
        }
        
        private void OnHideContextMenu(InputAction.CallbackContext obj)
        {
            inputActionsSource.InputActions.ContextMenu.Disable();
            
            _canNavigate = false;
            
            var startScale = 1.0f; 
            overlaySource.ContextMenu.style.scale = new StyleScale(new Scale(Vector3.one * startScale));
            var endScale = 0.1f;
            overlaySource.ContextMenu
                .DOScale(endScale, 0.25f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    overlaySource.ContextMenu.style.display = DisplayStyle.None;
                    inputActionsSource.InputActions.Player.Enable();
                    _canNavigate = true;
                });
        }

    }
}