namespace Game.Code.Infrastructure.TickManaging
{
    public interface ITickSource
    {
        void AddListener(ITickListener listener);
        void RemoveListener(ITickListener listener);
    }
}