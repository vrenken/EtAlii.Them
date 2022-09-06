namespace Game.World
{
    using UnityEditor;
    using UnityEngine;

    public class HexDebug : MonoBehaviour
    {
        private HexCoordinates hexCoordinates;

        private void Awake()
        {
            hexCoordinates = GetComponent<HexCoordinates>();
        }
        
        void OnDrawGizmos()
        {
            if (hexCoordinates != null)
            {
                var coordinates = hexCoordinates.OffsetCoordinates;
                Handles.Label(transform.position + Vector3.left * 0.5f, coordinates.ToString());
                //Handles.Label(transform.position, $"({coordinates.z}, {coordinates.y}, {coordinates.x})");
            }
        }
    }
}