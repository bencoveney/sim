namespace Sim.Ecs
{
  interface System
  {
    Filter GetFilter();
    void Update(EntityPool entityPool, int deltaTicks, int totalTicks);
  }
}