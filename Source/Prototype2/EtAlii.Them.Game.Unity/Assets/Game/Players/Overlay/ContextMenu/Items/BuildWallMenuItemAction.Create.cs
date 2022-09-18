namespace Game.Players
{
    using System;
    using System.Linq;
    using Game.Buildings;
    using Game.World;
    using UnityEngine;

    public partial class BuildWallMenuItemAction 
    {
        public override void Invoke(HexGrid grid, HexTile tile, object preparations)
        {
            ClearTile(tile);
            
            var neighbouringWalls = (HexTile[])preparations;

            CreateRightWall(grid, tile, neighbouringWalls, true); 
        }

        private void CreateRightWall(HexGrid grid, HexTile tile, HexTile[] neighbours, bool traverse)
        {
            switch(neighbours.Length)
            {
                case 0: CreateStandaloneWall(tile); break;
                case 1: CreateAndConnectWithOneWall(grid, tile, neighbours[0], traverse); break;
                case 2: CreateAndConnectWithTwoWalls(grid, tile, neighbours[0], neighbours[1], traverse); break;
                default: throw new NotSupportedException("Invalid wall");
            }
        }

        private void CreateAndConnectWithTwoWalls(HexGrid grid, HexTile tile, HexTile first, HexTile second, bool traverse)
        {
            var firstDirection = HexMath.RelativePositionTo(tile, first);
            var secondDirection = HexMath.RelativePositionTo(tile, second);

            var (prefab, rotation) = (firstDirection, secondDirection) switch
            {
                (HexDirection.East, HexDirection.West) => (straightWallPropPrefab, 0),
                (HexDirection.NorthLeft, HexDirection.SouthRight) => (straightWallPropPrefab, 60),
                (HexDirection.NorthRight, HexDirection.SouthLeft) => (straightWallPropPrefab, 120),

                // (HexDirection.West, HexDirection.NorthLeft) => (hexCornerWallBPropPrefab, 0),
                // (HexDirection.NorthLeft, HexDirection.NorthRight) => (hexCornerWallBPropPrefab, 60),
                // (HexDirection.NorthRight, HexDirection.East) => (hexCornerWallBPropPrefab, 120),
                // (HexDirection.East, HexDirection.SouthRight) => (hexCornerWallBPropPrefab, 180),
                // (HexDirection.SouthRight, HexDirection.SouthLeft) => (hexCornerWallBPropPrefab, 240),
                // (HexDirection.SouthLeft, HexDirection.West) => (hexCornerWallBPropPrefab, 300),

                (HexDirection.West, HexDirection.NorthRight) => (hexCornerWallAPropPrefab, 0),
                (HexDirection.NorthLeft, HexDirection.East) => (hexCornerWallAPropPrefab, 60),
                (HexDirection.NorthRight, HexDirection.SouthRight) => (hexCornerWallAPropPrefab, 120),
                (HexDirection.East, HexDirection.SouthLeft) => (hexCornerWallAPropPrefab, 180),
                (HexDirection.SouthRight, HexDirection.West) => (hexCornerWallAPropPrefab, 240),
                (HexDirection.SouthLeft, HexDirection.NorthLeft) => (hexCornerWallAPropPrefab, 300),

                (HexDirection.NorthRight, HexDirection.West) => (hexCornerWallAPropPrefab, 0),
                (HexDirection.East, HexDirection.NorthLeft) => (hexCornerWallAPropPrefab, 60),
                (HexDirection.SouthRight, HexDirection.NorthRight) => (hexCornerWallAPropPrefab, 120),
                (HexDirection.SouthLeft, HexDirection.East) => (hexCornerWallAPropPrefab, 180),
                (HexDirection.West, HexDirection.SouthRight) => (hexCornerWallAPropPrefab, 240),
                (HexDirection.NorthLeft, HexDirection.SouthLeft) => (hexCornerWallAPropPrefab, 300),

                _ => throw new NotSupportedException("Invalid wall prefab")
            };         
            
            var quaternion = Quaternion.AngleAxis(rotation, Vector3.up);
            var newTransform = tile.props.transform;
            var prop = Instantiate(prefab, newTransform.position, quaternion, newTransform);
            prop.name = prefab.name;
            tile.type = TileType.Wall;

            if (traverse)
            {
                UpdateTile(first, grid);
                UpdateTile(second, grid);
            }
        }

        private void CreateStandaloneWall(HexTile tile)
        {
            var prop = Instantiate(singleWallPropPrefab, tile.props.transform);
            prop.name = singleWallPropPrefab.name;
            tile.type = TileType.Wall;
        }

        private void CreateAndConnectWithOneWall(HexGrid hexGrid, HexTile tile, HexTile other, bool traverse)
        {
            var direction = HexMath.RelativePositionTo(tile, other);

            var newTileRotation = HexMath.ToDegrees(direction);
            var newTileQuaterion = Quaternion.AngleAxis(newTileRotation, Vector3.up);
            var newTransform = tile.props.transform;
            var prop = Instantiate(endWallPropPrefab, newTransform.position, newTileQuaterion, newTransform);
            prop.name = endWallPropPrefab.name;
            tile.type = TileType.Wall;

            if (traverse)
            {
                UpdateTile(other, hexGrid);
            }
        }

        private void UpdateTile(HexTile tile, HexGrid grid)
        {
            var neighbouringWalls = FindNeighbouringWalls(tile, grid);
            ClearTile(tile);
            CreateRightWall(grid, tile, neighbouringWalls, false);
        }
    }
}