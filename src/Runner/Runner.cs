using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sim.Ecs;
using Sim.World;

namespace Sim
{
  class Runner
  {
    private IEnumerable<Ecs.System> systems;
    private EntityPool entityPool;
    private IEnumerable<Filter> filters;
    public const int TickSize = 1;

    public Runner(EntityPool entityPool, IEnumerable<Ecs.System> systems)
    {
      this.systems = systems;
      this.entityPool = entityPool;
      this.filters = systems.Select(system => system.GetFilter()).ToList();
    }

    public void runFor(int years)
    {
      var ticksPerYear = Ticks.From(1, 0, 0, 0, 0);

      Stopwatch sw = new Stopwatch();
      for (var year = 0; year < years; year++)
      {
        sw.Restart();
        for (var tick = 0; tick < ticksPerYear; tick++)
        {
          UpdateFilters();
          foreach (Ecs.System system in systems)
          {
            system.Update(entityPool, TickSize, currentTick);
          }
          // Should this be done before update?
          currentTick += TickSize;
        }
        sw.Stop();
        Console.WriteLine($"Year {year} ran in {sw.ElapsedMilliseconds} ms - Population: {AliveFilter.Alive.GetEntities().Count()} - Total Entities: {entityPool.GetEntities().Count()}");
      }
    }

    private void UpdateFilters()
    {
      foreach (Entity entity in Updated.Get())
      {
        foreach (Filter filter in this.filters)
        {
          filter.OnComponentChanged(entity);
        }
      }
      Updated.Clear();
    }

    public int currentTick { get; set; } = 0;
  }
}