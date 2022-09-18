namespace Game.Players
{
    using System;
    using System.Linq;
    using Game.Buildings;
    using Game.World;
    using UnityEngine;

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

        private void ClearTile(HexTile tile)
        {
            var existingProps = tile.props.GetComponentsInChildren<BuildingProp>();
            foreach (var existingProp in existingProps)
            {
                Destroy(existingProp.gameObject); 
            }
        }
        public override void Invoke(HexTile tile, HexGrid grid, object preparations)
        {
            ClearTile(tile);
            
            var neighbouringWalls = (HexTile[])preparations;

            CreateRightWall(tile, neighbouringWalls); 
            tile.type = TileType.Wall;
        }

        private void CreateRightWall(HexTile tile, HexTile[] neighbours)
        {
            switch(neighbours.Length)
            {
                case 0: Instantiate(singleWallPropPrefab, tile.props.transform); break;
                case 1: CreateAndConnectWithOneWall(tile, neighbours.Single()); break;
                case 2: CreateAndConnectWithTwoWalls(tile, neighbours); break;
                default: throw new NotSupportedException("Invalid wall");
            }
        }

        private HexTile[] FindNeighbouringWalls(HexTile tile, HexGrid grid)
        {
            var neighbours = grid.GetNeighbours(tile.HexCoordinates);
            return neighbours
                .Select(grid.GetTile)
                .Where(t => t.type == TileType.Wall)
                .ToArray();
        }

        private void CreateAndConnectWithTwoWalls(HexTile tile, HexTile[] neighbours)
        {
            Instantiate(straightWallPropPrefab, tile.props.transform);
        }

        private void CreateAndConnectWithOneWall(HexTile tile, HexTile other)
        {
            var direction = HexMath.RelativePositionTo(tile, other);

            var newTileRotation = HexMath.ToDegrees(direction);
            var newTileQuaterion = Quaternion.AngleAxis(newTileRotation, Vector3.up);
            var newTransform = tile.props.transform;
            Instantiate(endWallPropPrefab, newTransform.position, newTileQuaterion, newTransform);

            ClearTile(other);

            var otherTileRotation = HexMath.ToDegrees(direction, true);
            var otherTileQuaterion = Quaternion.AngleAxis(otherTileRotation, Vector3.up);
            var otherTransform = other.props.transform;
            Instantiate(endWallPropPrefab, otherTransform.position, otherTileQuaterion, otherTransform);

            

        }

    }
}