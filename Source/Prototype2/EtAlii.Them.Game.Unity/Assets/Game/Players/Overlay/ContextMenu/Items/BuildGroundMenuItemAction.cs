namespace Game.Players
{
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildGroundMenuItemAction")]
    public class BuildGroundMenuItemAction : MenuItemAction
    {
        public HexTile groundTilePrefab;

        public override bool IsValid(HexTile tile, out int priority)
        {
            priority = 100;
            return tile.type switch
            {
                TileType.Beach => true,
                TileType.Sky => true,
                _ => false
            };
        }

        public override void Invoke(HexTile tile, HexGrid grid)
        {
            var coordinates = tile.HexCoordinates;

            grid.ReplaceTile(coordinates, groundTilePrefab);

        }
    }
}