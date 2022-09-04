namespace Game.World
{
    using System;
    using UnityEngine;

    public class HexCoordinates : MonoBehaviour
    {
        public static float xOffset = 2f;
        public static float yOffset = 1f;
        public static float zOffset = 1.73f;

        [Header("Offset coordinates")]
        [SerializeField] private Vector3Int offsetCoordinates;

        private void Awake()
        {
            offsetCoordinates = ConvertPositionToOffset(transform.position);
        }

        private Vector3Int ConvertPositionToOffset(Vector3 position)
        {
            var x = Mathf.CeilToInt(position.x / xOffset);
            var y = Mathf.RoundToInt(position.x / yOffset);
            var z = Mathf.RoundToInt(position.x / zOffset);
            return new Vector3Int(x, y, z);
        }
    }
}