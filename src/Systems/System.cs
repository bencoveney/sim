using Sim.Filters;

namespace Sim.Systems
{
  interface System
  {
    Filter GetFilter();
    void Update(float deltaMinutes, float totalMinutes);
  }
}