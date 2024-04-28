using System;

namespace Game.Scripts.Infrastructure.TickManaging
{
    public interface ITickListener
    {
        event Action<ITickListener> OnDisposed;
        
        void Tick(float deltaTime);
    }
}