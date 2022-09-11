namespace Game.World
{
    using System;
    using UnityEngine;

    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public LayerMask selectionMask;

        public HexGrid grid;
        private Vector3Int[] items = Array.Empty<Vector3Int>();

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        public void HandleClick(Vector3 mousePosition)
        {
            if (FindTarget(mousePosition, out GameObject result))
            {
                var hex = result.GetComponent<HexTile>();

                foreach (var item in items)
                {
                    grid.GetTile(item).DisableHighlight();
                }
                items = grid.GetNeighbours(hex.HexCoordinates);
                foreach (var item in items)
                {
                    grid.GetTile(item).EnableHighlight();
                }
            }
        }

        private bool FindTarget(Vector3 mousePosition, out GameObject result)
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, selectionMask))
            {
                result = hit.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}