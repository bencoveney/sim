namespace EntityComponentSystem
{
    public interface System
    {
        void Update(Ecs ecs, int deltaTicks, int totalTicks);
    }
}