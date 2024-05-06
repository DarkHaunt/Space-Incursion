using System;

namespace Game.Scripts.Infrastructure.TickManaging
{
    public interface ITickListener
    {
        void Tick(float deltaTime);
    }
}