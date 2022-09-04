namespace Game.World
{
    using UnityEngine;

    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        private HexCoordinates hexCoordinates;

        public Vector3Int HexCoordinates => hexCoordinates.OffsetCoordinates;

        private void Awake()
        {
            hexCoordinates = GetComponent<HexCoordinates>();
        }
    }
}