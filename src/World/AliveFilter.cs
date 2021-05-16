using System.Collections.Generic;
using Sim.Ecs;

namespace Sim.World
{
  class AliveFilter
  {
    public static Filter Alive = new Filter(
      new List<ComponentName> { ComponentName.Birth },
      new List<ComponentName> { ComponentName.Death }
    );
  }
}