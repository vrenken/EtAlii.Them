namespace Game.World
{
    using System;
    using UnityEngine;

    public static class HexMath
    {
        public static readonly Vector3Int[] DirectionsOffsetOdd = 
        {
            new(-1, 0, 1), // N1
            new(0, 0, 1), // N2
            new(1, 0, 0), // E
            new(0, 0, -1), // S2
            new(-1, 0, -1), // S1
            new(-1, 0, 0), // W
        };
        
        public static readonly Vector3Int[] DirectionsOffsetEven = 
        {
            new(0, 0, 1), // N1
            new(1, 0, 1), // N2
            new(1, 0, 0), // E
            new(1, 0, -1), // S2
            new(0, 0, -1), // S1
            new(-1, 0, 0), // W
        };

        public static int ToDegrees(HexDirection direction, bool invert = false)
        {
            if (invert)
            {
                return direction switch
                {
                    HexDirection.NorthLeft => -120,
                    HexDirection.NorthRight => -60,
                    HexDirection.East => 0,
                    HexDirection.SouthRight => +60,
                    HexDirection.SouthLeft => +120,
                    HexDirection.West => -180,
                    _ => throw new InvalidOperationException("Not supported direction")
                };
            }
            
            return direction switch
            {
                HexDirection.NorthLeft => +60,
                HexDirection.NorthRight => +120,
                HexDirection.East => 180,
                HexDirection.SouthRight => -120,
                HexDirection.SouthLeft => -60,
                HexDirection.West => 0,
                _ => throw new InvalidOperationException("Not supported direction")
            };
        }
        
        public static HexDirection RelativePositionTo(HexTile from, HexTile to)
        {
            var delta = to.HexCoordinates - from.HexCoordinates;

            var isOdd = to.HexCoordinates.z % 2 == 0;
            if (isOdd)
            {
                if (delta == DirectionsOffsetOdd[0]) return HexDirection.NorthLeft;
                if (delta == DirectionsOffsetOdd[1]) return HexDirection.NorthRight;
                if (delta == DirectionsOffsetOdd[2]) return HexDirection.East;
                if (delta == DirectionsOffsetOdd[3]) return HexDirection.SouthRight;
                if (delta == DirectionsOffsetOdd[4]) return HexDirection.SouthLeft;
                if (delta == DirectionsOffsetOdd[5]) return HexDirection.West;
                return HexDirection.Unknown;
            }
            if (delta == DirectionsOffsetEven[0]) return HexDirection.NorthLeft;
            if (delta == DirectionsOffsetEven[1]) return HexDirection.NorthRight;
            if (delta == DirectionsOffsetEven[2]) return HexDirection.East;
            if (delta == DirectionsOffsetEven[3]) return HexDirection.SouthRight;
            if (delta == DirectionsOffsetEven[4]) return HexDirection.SouthLeft;
            if (delta == DirectionsOffsetEven[5]) return HexDirection.West;
            return HexDirection.Unknown;
        }
    }
}