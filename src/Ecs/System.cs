namespace Sim.Ecs
{
    interface System
    {
        void Update(EntityPool entityPool, int deltaTicks, int totalTicks);
    }
}