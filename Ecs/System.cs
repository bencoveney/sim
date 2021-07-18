namespace Ecs
{
    public interface System
    {
        void Update(EntityPool entityPool, int deltaTicks, int totalTicks);
    }
}