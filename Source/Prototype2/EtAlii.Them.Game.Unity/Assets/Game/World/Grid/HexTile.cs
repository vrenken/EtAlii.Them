namespace Game.World
{
    using UnityEngine;

    [SelectionBase]
    public class HexTile : MonoBehaviour
    {
        private HexCoordinates hexCoordinates;
        private GlowHighlight highlight;

        public TileType type;

        public GameObject props;
        
        public HexTile Clone(HexTile tilePrefab)
        {
            var sourceTileTransform = transform;
            var newTile = Instantiate(tilePrefab, sourceTileTransform.position, Quaternion.identity, sourceTileTransform.parent);
            newTile.hexCoordinates = hexCoordinates;
            newTile.name = $"{tilePrefab.name} {HexCoordinates}";
            return newTile;
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