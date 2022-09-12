namespace Game.Players
{
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class ContextMenu : MonoBehaviour
    {
        public HexTileSelector hexTileSelector;
        private UIDocument _contextMenu;

        public HexTile tilePrefab;

        public InputActionsSource inputActionsSource;
        private void Awake()
        {
            _contextMenu = GetComponent<UIDocument>();
            _contextMenu.enabled = false;
        }

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
            _contextMenu.enabled = true;
            inputActionsSource.InputActions.Player.Disable();
            inputActionsSource.InputActions.ContextMenu.Enable();
        }

        private void OnHideContextMenu(InputAction.CallbackContext obj)
        {
            inputActionsSource.InputActions.ContextMenu.Disable();
            inputActionsSource.InputActions.Player.Enable();
            _contextMenu.enabled = false;
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
