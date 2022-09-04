namespace Game.World
{
    using UnityEngine;

    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        private HexCoordinates hexCoordinates;
        private GlowHighlight highlight;

        public Vector3Int HexCoordinates => hexCoordinates.OffsetCoordinates;

        private void Awake()
        {
            hexCoordinates = GetComponent<HexCoordinates>();
            highlight = GetComponent<GlowHighlight>();
        }

        public void EnableHighlight() => highlight.ToggleGlow(true);
        public void DisableHighlight() => highlight.ToggleGlow(false);
    }
}