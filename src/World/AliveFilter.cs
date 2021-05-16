using System.Collections.Generic;
using Sim.Ecs;

namespace Sim.World
{
  class AliveFilter
  {
    public static Filter Alive = new Filter(
      new List<int> { ComponentKind.Birth.ToInt() },
      new List<int> { ComponentKind.Death.ToInt() }
    );
  }
}