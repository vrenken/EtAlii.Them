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
                Handles.Label(transform.position, coordinates.ToString());
            }
        }
    }
}