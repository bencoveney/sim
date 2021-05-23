using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sim.Ecs;
using Sim.World;

namespace Sim.Runner
{
  class Runner
  {
    private EntityPool entityPool;
    private List<Filter> filters = new List<Filter>();
    private Dictionary<Frequency, List<TimedSystem>> systems = new Dictionary<Frequency, List<TimedSystem>>();
    public const int TickSize = 1;

    public Runner(EntityPool entityPool)
    {
      this.entityPool = entityPool;
      foreach (Frequency frequency in (Frequency[])Enum.GetValues(typeof(Frequency)))
      {
        systems[frequency] = new List<TimedSystem>();
      }
    }
    public void AddSystem(Ecs.System system, Frequency frequency)
    {
      systems[frequency].Add(new TimedSystem() { Stopwatch = new Stopwatch(), System = system, Name = system.GetType().Name });
      this.filters.Add(system.GetFilter());
    }
    public void runFor(int years)
    {
      Stopwatch sw = new Stopwatch();
      for (var year = 0; year < years; year++)
      {
        sw.Restart();
        for (var tick = 0; tick < Ticks.PerYear; tick += TickSize)
        {
          this.RunTick();
        }
        sw.Stop();
        var builder = new StringBuilder();
        builder.AppendLine($"{Environment.NewLine}Year {year}");
        builder.AppendLine($"- Ran in {sw.ElapsedMilliseconds} ms");
        builder.AppendLine($"- Population: {AliveFilter.Alive.GetEntities().Count()}");
        builder.AppendLine($"- Total Entities: { entityPool.GetEntities().Count()}");
        builder.AppendLine($"- Systems:");
        foreach (TimedSystem system in systems.Values.SelectMany(systems => systems))
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

    private void RunTick()
    {
      UpdateFilters();
      currentTick += TickSize;
      if (currentTick % Ticks.PerYear == 0)
      {
        RunSystems(this.systems[Frequency.Year]);
      }
      if (currentTick % Ticks.PerMonth == 0)
      {
        RunSystems(this.systems[Frequency.Month]);
      }
      if (currentTick % Ticks.PerDay == 0)
      {
        RunSystems(this.systems[Frequency.Day]);
      }
      if (currentTick % Ticks.PerHour == 0)
      {
        RunSystems(this.systems[Frequency.Hour]);
      }
      if (currentTick % Ticks.PerMinute == 0)
      {
        RunSystems(this.systems[Frequency.Minute]);
      }
      RunSystems(this.systems[Frequency.Tick]);
    }
    private void RunSystems(List<TimedSystem> timedSystems)
    {
      foreach (TimedSystem timedSystem in timedSystems)
      {
        timedSystem.Stopwatch.Start();
        timedSystem.System.Update(entityPool, TickSize, currentTick);
        timedSystem.Stopwatch.Stop();
      }
    }

    private void RunTickForFrequency(Frequency frequency)
    {

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