namespace Game.World
{
    using UnityEngine;

    [SelectionBase]
    public class HexTile : MonoBehaviour
    {
        private HexCoordinates hexCoordinates;
        private GlowHighlight highlight;

        public void Clone(HexTile sourceTile)
        {
            hexCoordinates = sourceTile.hexCoordinates;
            name = sourceTile.name;
            var sourceTileTransform = sourceTile.transform;
            var targetTileTransform = transform;
            targetTileTransform.SetParent(sourceTileTransform.parent);
            targetTileTransform.position = sourceTileTransform.position;
        } 
        
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