namespace Game.Players
{
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildGroundMenuItemAction")]
    public class BuildGroundMenuItemAction : MenuItemAction
    {
        public HexTile groundTilePrefab;

        public override bool IsValid(HexGrid grid, HexTile tile, out int priority, out object preparations)
        {
            preparations = null;
            priority = 100;
            return tile.type switch
            {
                TileType.Beach => true,
                TileType.Sky => true,
                _ => false
            };
        }

        public override void Invoke(HexGrid grid, HexTile tile, object preparations)
        {
            var coordinates = tile.HexCoordinates;

            grid.ReplaceTile(coordinates, groundTilePrefab);
        }
    }
}