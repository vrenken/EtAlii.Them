using EtAlii.Them.Game.FixedMaths;

namespace EtAlii.Them.Game.Physics.Engines
{
    public interface IScheduledGraphicsEngine
    {
        string Name { get; }
        void   Draw(in FixedPoint normalisedDelta);
    }
}