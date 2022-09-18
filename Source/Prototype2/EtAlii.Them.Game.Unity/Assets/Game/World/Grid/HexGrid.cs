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

            var directions = coordinates.z % 2 == 0 
                ? HexMath.DirectionsOffsetEven 
                : HexMath.DirectionsOffsetOdd;

            foreach (var direction in directions)
            {
                if(_tiles.ContainsKey(coordinates + direction))
                {
                    newNeighbors.Add(coordinates + direction);
                }
            }

            return _tileNeighbours[coordinates] = newNeighbors.ToArray();
        }
    }
}
