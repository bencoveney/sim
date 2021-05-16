using System.Collections.Generic;
using Sim.Ecs;

namespace Sim.World
{
  class PositionSystem : Ecs.System
  {
    private Filter filter = new Filter(
      new List<ComponentName> { ComponentName.Position }
    );
    public Filter GetFilter()
    {
      return this.filter;
    }
    public void Update(float deltaTime, float currentTick)
    {
      foreach (Entity entity in this.filter.GetEntities())
      {

      }
    }
  }
}