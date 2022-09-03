namespace Game.World
{
    using System;
    using System.Collections.Generic;
    using Game.Buildings;

    public class BuildingsMap
    {
        private readonly Dictionary<BuildingPosition, Building> _buildings = new();

        public Building this[uint x, uint y]  { get => Get(new BuildingPosition { X = x, Y = y }); set => Add(new BuildingPosition { X = x, Y = y }, value); }

        public Building this[BuildingPosition position] { get => Get(position);  set => Add(position, value); }

        private Building Get(BuildingPosition position)
        {
            _buildings.TryGetValue(position, out var building);
            return building;
        }

        private void Add(BuildingPosition position, Building building)
        {
            if (_buildings.ContainsKey(position))
            {
                throw new InvalidOperationException($"Building already in position on {position.X}x{position.Y}");
            }

            _buildings[position] = building;
        }
    }
}