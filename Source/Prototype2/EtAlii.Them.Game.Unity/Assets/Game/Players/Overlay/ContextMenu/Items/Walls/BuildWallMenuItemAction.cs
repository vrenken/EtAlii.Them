namespace Game.Players
{
    using System.Linq;
    using Game.Buildings;
    using Game.World;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildWallMenuItemAction")]
    public partial class BuildWallMenuItemAction : MenuItemAction
    {
        public BuildingProp straightWallPropPrefab;
        public BuildingProp cornerWallPropPrefab;
        public BuildingProp gateWallPropPrefab;
        public BuildingProp closedGateWallPropPrefab;
        public BuildingProp hexCornerWallAPropPrefab;
        public BuildingProp hexCornerWallBPropPrefab;
        public BuildingProp singleWallPropPrefab;
        public BuildingProp endWallPropPrefab;
        
        private HexTile[] FindNeighbouringWalls(HexTile tile, HexGrid grid)
        {
            var neighbours = grid.GetNeighbours(tile.HexCoordinates);
            return neighbours
                .Select(grid.GetTile)
                .Where(t => t.type == TileType.Wall)
                .ToArray();
        }
    }
}