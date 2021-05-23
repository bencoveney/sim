using System.Collections.Generic;
using Sim.Ecs;
using Sim.Logging;
using Sim.Runner;
using Sim.World;

namespace Sim
{
  internal class Program
  {
    private static void Main()
    {
      var start = Ticks.From(50, 0, 0, 0, 0);

      var entityPool = new EntityPool();

      WorldFactory.Create(entityPool, start, 3, 10);

      Logger.LogEntities("Before running", entityPool.GetEntities(), start);

      var runner = new Runner.Runner(entityPool);
      runner.AddSystem(new DeathSystem(), Frequency.Day);
      runner.currentTick = start;
      runner.runFor(100);

      Logger.LogEntities("After running", entityPool.GetEntities(), runner.currentTick);
    }
  }
}