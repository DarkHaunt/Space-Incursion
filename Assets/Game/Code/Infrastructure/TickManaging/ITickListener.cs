namespace Game.Code.Infrastructure.TickManaging
{
    public interface ITickListener
    {
        void Tick(float deltaTime);
    }
}