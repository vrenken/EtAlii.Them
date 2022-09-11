namespace Game.World
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class HexGrid : MonoBehaviour
    {
        private readonly Dictionary<Vector3Int, HexTile> _tiles = new();
        private readonly Dictionary<Vector3Int, Vector3Int[]> _tileNeighbours = new();

        private void Start()
        {
            foreach (var tile in GetComponentsInChildren<HexTile>())
            {
                _tiles[tile.HexCoordinates] = tile;
            }
        }

        public HexTile GetTile(Vector3Int coordinates)
        {
            _tiles.TryGetValue(coordinates, out var hex);
            return hex;
        }

        public void ReplaceTile(Vector3Int coordinates, HexTile tilePrefab)
        {
            if (_tiles.TryGetValue(coordinates, out var oldTile))
            {
                var newTile = oldTile.Clone(tilePrefab);
                oldTile.DisableHighlight();
                Destroy(oldTile.gameObject);
                _tiles[newTile.HexCoordinates] = newTile;
            }
            else
            {
                throw new NotSupportedException("Adding new tiles to the grid is not supported");
            }
        }
        
        public Vector3Int[] GetNeighbours(Vector3Int coordinates)
        {
            if (!_tiles.ContainsKey(coordinates))
            {
                return Array.Empty<Vector3Int>();
            }
            if(_tileNeighbours.TryGetValue(coordinates, out var neighbors))
            {
                return neighbors;
            }

            var newNeighbors = new List<Vector3Int>();

            foreach (var direction in Direction.GetDirectionsOffsets(coordinates.z))
            {
                if(_tiles.ContainsKey(coordinates + direction))
                {
                    newNeighbors.Add(coordinates + direction);
                }
            }

            return _tileNeighbours[coordinates] = newNeighbors.ToArray();
        }
    }

    public static class Direction
    {
        private static readonly Vector3Int[] directionsOffsetOdd = 
        {
            new(-1, 0, 1), // N1
            new(0, 0, 1), // N2
            new(1, 0, 0), // E
            new(0, 0, -1), // S2
            new(-1, 0, -1), // S1
            new(-1, 0, 0), // W
        };
        
        private static readonly Vector3Int[] directionsOffsetEven = 
        {
            new(0, 0, 1), // N1
            new(1, 0, 1), // N2
            new(1, 0, 0), // E
            new(1, 0, -1), // S2
            new(0, 0, -1), // S1
            new(-1, 0, 0), // W
        };

        public static Vector3Int[] GetDirectionsOffsets(int z)
        {
            return z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
        }
    }
}
