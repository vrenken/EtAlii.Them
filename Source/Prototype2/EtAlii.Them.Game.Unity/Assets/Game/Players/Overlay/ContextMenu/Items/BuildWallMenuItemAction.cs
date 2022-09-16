namespace Game.Players
{
    using System;
    using System.Linq;
    using Game.Buildings;
    using Game.World;using UnityEngine;

    [CreateAssetMenu(menuName = "Them/ContextMenu/BuildWallMenuItemAction")]
    public class BuildWallMenuItemAction : MenuItemAction
    {
        public BuildingProp straightWallPropPrefab;
        public BuildingProp cornerWallPropPrefab;
        public BuildingProp gateWallPropPrefab;
        public BuildingProp closedGateWallPropPrefab;
        public BuildingProp hexCornerWallAPropPrefab;
        public BuildingProp hexCornerWallBPropPrefab;
        public BuildingProp singleWallPropPrefab;
        public BuildingProp endWallPropPrefab;

        public override bool IsValid(HexTile tile, HexGrid grid, out int priority, out object preparations)
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

        public override void Invoke(HexTile tile, HexGrid grid, object preparations)
        {
            var existingProps = tile.props.GetComponentsInChildren<BuildingProp>();
            foreach (var existingProp in existingProps)
            {
                Destroy(existingProp.gameObject); 
            }

            var neighbouringWalls = (HexTile[])preparations;

            switch (neighbouringWalls.Length)
            {
                case 0:
                    Instantiate(singleWallPropPrefab, tile.props.transform);
                    break;
                case 1:
                    Instantiate(endWallPropPrefab, tile.props.transform);
                    break;
                case 2:
                    Instantiate(straightWallPropPrefab, tile.props.transform);
                    break;
                default:
                    throw new NotSupportedException("Invalid wall");
            }
            tile.type = TileType.Wall;
        }

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