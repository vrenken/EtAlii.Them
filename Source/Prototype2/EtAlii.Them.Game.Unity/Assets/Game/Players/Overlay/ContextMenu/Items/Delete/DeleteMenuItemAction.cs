namespace Game.Players
{
    using Game.World;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/DeleteMenuItemAction")]
    public class DeleteMenuItemAction : MenuItemAction
    {
        public HexTile groundTilePrefab;

        public override bool IsValid(HexGrid grid, HexTile tile, out int priority, out object preparations)
        {
            preparations = null;
            priority = 0;
            var canDelete = tile.type switch
            {
                TileType.Ground => false,
                TileType.Sky => false,
                _ => true
            };

            return canDelete;
        }
        public override void Invoke(HexGrid grid, HexTile tile, object preparations)
        {
            tile.Clear();

            var coordinates = tile.HexCoordinates;

            grid.ReplaceTile(coordinates, groundTilePrefab);
        }
    }
}