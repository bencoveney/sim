
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Filters
{
  class AliveFilter
  {
    public static Filter Alive = new Filter(
      new List<ComponentName> { ComponentName.Birth },
      new List<ComponentName> { ComponentName.Death }
    );
  }
}