using System;

namespace EtAlii.Them.Game
{
    using EtAlii.Them.Game.Graphics;

    public interface IGameLoop
    {
        void      Stop();
        void      Execute();
        IGameLoop AddGraphics(IGraphics graphics);
        IGameLoop AddInput(IInput input);
        IGameLoop SetPhysicsSimulationsPerSecond(uint frequency);
        IGameLoop SetGraphicsFramesPerSecond(uint frequency);
        IGameLoop SetUncappedGraphicsFramesPerSecond();
        IGameLoop SetSimulationSpeed(float simulationSpeed);
        IGameLoop SetSchedulers(IEngineScheduler logicScheduler, IEngineSchedulerReporter logicSchedulerReporter = null);
    }
}