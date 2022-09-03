using EtAlii.Them.Game.FixedMaths;

namespace EtAlii.Them.Game
{
    using EtAlii.Them.Game.Physics.Engines;

    public interface IEngineScheduler
    {
        void ExecuteGraphics(FixedPoint delta);
        void ExecutePhysics(FixedPoint delta);
        void RegisterScheduledGraphicsEngine(IScheduledGraphicsEngine scheduledGraphicsEngine);
        void RegisterScheduledPhysicsEngine(IScheduledPhysicsEngine scheduled);
    }
}