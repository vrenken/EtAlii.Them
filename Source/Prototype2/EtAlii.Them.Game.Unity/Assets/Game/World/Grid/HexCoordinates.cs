namespace Game.World
{
    using UnityEngine;

    public class HexCoordinates : MonoBehaviour
    {
        public static float xOffset = 2f;
        public static float yOffset = 1f;
        public static float zOffset = 1.73f;

        internal Vector3Int OffsetCoordinates => offsetCoordinates;
        [Header("Offset coordinates")]
        [SerializeField] private Vector3Int offsetCoordinates;

        private void Awake()
        {
            offsetCoordinates = ConvertPositionToOffset(transform.position);
            
#if UNITY_EDITOR
            var components = GetComponents<HexCoordinates>();
            if (components.Length != 1)
            {
                Debug.LogError($"GameObject {name} has more than one {nameof(HexCoordinates)}");
            }
#endif
        }

        private Vector3Int ConvertPositionToOffset(Vector3 position)
        {
            var x = Mathf.CeilToInt((position.x - 0.5f) / xOffset); // Small correction to cope with Tessera tile positioning.
            var y = Mathf.CeilToInt(position.y / yOffset);
            var z = Mathf.CeilToInt(position.z / zOffset);
            return new Vector3Int(x, y, z);
        }
    }
}