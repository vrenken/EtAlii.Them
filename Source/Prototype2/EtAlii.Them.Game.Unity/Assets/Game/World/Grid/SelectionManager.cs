namespace Game.World
{
    using UnityEngine;

    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public LayerMask selectionMask;

        public HexGrid grid;
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
                var hex = result.GetComponent<Hex>();

                var neighbors = grid.GetNeighbours(hex.HexCoordinates);
                Debug.Log($"Neighbors for ({hex.HexCoordinates}) are:");
                foreach (var neighbor in neighbors)
                {
                    Debug.Log($"({neighbor})");
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