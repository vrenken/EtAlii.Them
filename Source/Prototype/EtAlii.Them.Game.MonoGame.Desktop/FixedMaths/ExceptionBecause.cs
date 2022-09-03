using System;

namespace EtAlii.Them.Game.FixedMaths
{
    public static class ExceptionBecause
    {
        public static Exception LargeSqrMagnitude(float x, float y, float magnitude)
        {
            return new Exception($"Generated Large SqrMagnitude, {x}, {y} = {magnitude}");
        }
    }
}