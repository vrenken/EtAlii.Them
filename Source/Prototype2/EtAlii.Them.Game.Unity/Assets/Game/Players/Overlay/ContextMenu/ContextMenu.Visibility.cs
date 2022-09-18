namespace Game.Players
{
    using System.Linq;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;
    using DG.Tweening;

    public partial class ContextMenu
    {
        private const float SmallScale = 0.7f;
        private const float LargeScale = 1.0f;
        private const float SmallOpacity = 0.1f;
        private const float LargeOpacity = 1.0f;
        private const float ZoomDuration = 0.15f;
        
        private void OnShowContextMenu(InputAction.CallbackContext context)
        {
            if (hexTileSelector.hexTile == null) return;
            if (!_activeItems.Any()) return;
            
            SetMenuItems();
            SetPosition();

            inputActionsSource.InputActions.Player.Disable();

            overlaySource.ContextMenu.style.display = DisplayStyle.Flex;

            _canNavigate = false;
            overlaySource.ContextMenu
                .DoFade(SmallOpacity, LargeOpacity, ZoomDuration)
                .SetEase(Ease.Linear);
            overlaySource.ContextMenu
                .DOScale(SmallScale, LargeScale, ZoomDuration)
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
            
            overlaySource.ContextMenu
                .DoFade(LargeOpacity, SmallOpacity, ZoomDuration)
                .SetEase(Ease.Linear);
            overlaySource.ContextMenu
                .DOScale(LargeScale, SmallScale, ZoomDuration)
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