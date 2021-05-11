using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sim.Model;

namespace Sim
{
  class Runner
  {
    private List<Systems.System> systems;
    private List<Entity> entities;

    public Runner(List<Systems.System> systems, List<Entity> entities)
    {
      this.systems = systems;
      this.entities = entities;
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
          systems.ForEach(system =>
          {
            system.Update(1, entities);
          });
          currentTick++;
        }
        sw.Stop();
        Console.WriteLine($"Year {year} ran in {sw.ElapsedMilliseconds} ms");
      }
    }

    public int currentTick { get; set; } = 0;
  }
}