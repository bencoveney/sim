using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sim.Ecs;
using Sim.World;

namespace Sim
{
  class Runner
  {
    private List<TimedSystem> systems;
    private EntityPool entityPool;
    private IEnumerable<Filter> filters;
    public const int TickSize = 1;

    public Runner(EntityPool entityPool, IEnumerable<Ecs.System> systems)
    {
      this.systems = systems.Select(system => new TimedSystem() { Stopwatch = new Stopwatch(), System = system, Name = system.GetType().Name }).ToList();
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
          foreach (TimedSystem system in systems)
          {
            system.Stopwatch.Start();
            system.System.Update(entityPool, TickSize, currentTick);
            system.Stopwatch.Stop();
          }
          // Should this be done before update?
          currentTick += TickSize;
        }
        sw.Stop();
        var builder = new StringBuilder();
        builder.AppendLine($"{Environment.NewLine}Year {year}");
        builder.AppendLine($"- Ran in {sw.ElapsedMilliseconds} ms");
        builder.AppendLine($"- Population: {AliveFilter.Alive.GetEntities().Count()}");
        builder.AppendLine($"- Total Entities: { entityPool.GetEntities().Count()}");
        builder.AppendLine($"- Systems:");
        foreach (TimedSystem system in systems)
        {
          builder.AppendLine($"  - {system.Name} ran in {system.Stopwatch.ElapsedMilliseconds}ms");
          system.Stopwatch.Reset();
        }
        Console.WriteLine(builder.ToString());
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

    private class TimedSystem
    {
      public Ecs.System System;
      public Stopwatch Stopwatch;
      public string Name;
    }
  }
}