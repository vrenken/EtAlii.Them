namespace Game.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using UnityEngine;

    public class HexGrid : MonoBehaviour
    {
        private readonly Dictionary<Vector3Int, Hex> _tiles = new();
        private readonly Dictionary<Vector3Int, Vector3Int[]> _tileNeighbours = new();

        private void Start()
        {
            foreach (var tile in GetComponentsInChildren<Hex>())
            {
                _tiles[tile.HexCoordinates] = tile;
            }
        }

        public Hex GetTile(Vector3Int coordinates)
        {
            _tiles.TryGetValue(coordinates, out var hex);
            return hex;
        }

        public Vector3Int[] GetNeighbours(Vector3Int coordinates)
        {
            if (_tiles.ContainsKey(coordinates))
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
            new Vector3Int(-1, 0, 1), // N1
            new Vector3Int(0, 0, 1), // N2
            new Vector3Int(1, 0, 0), // E
            new Vector3Int(0, 0, -1), // S2
            new Vector3Int(-1, 0, -1), // S1
            new Vector3Int(-1, 0, 0), // W
        };
        
        private static readonly Vector3Int[] directionsOffsetEven = 
        {
            new Vector3Int(0, 0, 1), // N1
            new Vector3Int(1, 0, 1), // N2
            new Vector3Int(1, 0, 0), // E
            new Vector3Int(1, 0, -1), // S2
            new Vector3Int(0, 0, -1), // S1
            new Vector3Int(-1, 0, 0), // W
        };

        public static Vector3Int[] GetDirectionsOffsets(int z)
        {
            return z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
        }
    }
}
