using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sim.Ecs;
using Sim.Filters;

namespace Sim
{
  class Runner
  {
    private IEnumerable<Ecs.System> systems;
    private IEnumerable<Entity> entities;
    private IEnumerable<Filter> filters;
    public const int TickSize = 1;

    public Runner(IEnumerable<Ecs.System> systems, IEnumerable<Entity> entities)
    {
      this.systems = systems;
      this.entities = entities;
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
            system.Update(TickSize, currentTick);
          }
          // Should this be done before update?
          currentTick += TickSize;
        }
        sw.Stop();
        Console.WriteLine($"Year {year} ran in {sw.ElapsedMilliseconds} ms - Population: {AliveFilter.Alive.GetEntities().Count()} - Total Entities: {entities.Count()}");
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