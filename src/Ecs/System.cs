namespace Sim.Ecs
{
  interface System
  {
    Filter GetFilter();
    void Update(float deltaMinutes, float totalMinutes);
  }
}