namespace Sim.Ecs
{
  interface System
  {
    Filter GetFilter();
    void Update(EntityPool entityPool, float deltaMinutes, float totalMinutes);
  }
}