using System;
using EtAlii.Them.Game.FixedMaths;

namespace EtAlii.Them.Game.Physics.Engines
{
    public interface IScheduledPhysicsEngine
    {
        string Name { get; }
        void   Execute(in FixedPoint delta);
    }
}