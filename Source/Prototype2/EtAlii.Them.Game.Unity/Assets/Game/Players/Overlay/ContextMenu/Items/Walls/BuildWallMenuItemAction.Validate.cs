namespace Game.Players
{
    using Game.World;

    public partial class BuildWallMenuItemAction 
    {
        public override bool IsValid(HexGrid grid, HexTile tile, out int priority, out object preparations)
        {
            preparations = null;
            priority = 100;
            var isGround = tile.type switch
            {
                TileType.Ground => true,
                _ => false
            };
            if (isGround)
            {
                var neighbouringWalls = FindNeighbouringWalls(tile, grid);
                preparations = neighbouringWalls;
                return neighbouringWalls.Length <= 2;
            }

            return false;
        }
    }
}