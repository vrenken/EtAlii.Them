namespace Game.Players
{
    using Game.World;
    using UnityEngine.InputSystem;
    using UnityEngine;

    public class ContextMenu : MonoBehaviour
    {
        private HexTileSelector _hexTileSelector;
        private PlayerInputActions _inputActions;

        public HexTile tilePrefab;

        private void Awake()
        {
            _hexTileSelector = GetComponent<HexTileSelector>();

            _inputActions = new PlayerInputActions();
            _inputActions.Player.Enable();
        }

        private void OnEnable()
        {
            _inputActions.Player.ContextMenu.performed += OnContextMenu;
        }

        private void OnDisable()
        {
            _inputActions.Player.ContextMenu.performed -= OnContextMenu;
        }

        private void OnContextMenu(InputAction.CallbackContext obj)
        {
            if (_hexTileSelector.hexTile != null)
            {
                var coordinates = _hexTileSelector.hexTile.HexCoordinates;

                var tile = Instantiate(tilePrefab);

                _hexTileSelector.hexGrid.ReplaceTile(coordinates, tile);
            }
        }
    }
}
