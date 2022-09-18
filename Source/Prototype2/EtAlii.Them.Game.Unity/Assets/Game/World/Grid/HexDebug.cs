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

#if UNITY_EDITOR
            var components = GetComponents<HexDebug>();
            if (components.Length != 1)
            {
                Debug.LogError($"GameObject {name} has more than one {nameof(HexDebug)}");
            }
#endif
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