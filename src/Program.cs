using System.Collections.Generic;
using Sim.Factories;

namespace Sim
{
  internal class Program
  {
    private static void Main()
    {
      var start = Ticks.From(50, 0, 0, 0, 0);

      var entities = World.Create((int)start, 3, 10);

      Logger.LogEntities("Before running", entities, start);

      var systems = new List<Ecs.System>()
      {
        new Systems.DeathSystem()
      };

      var runner = new Runner(systems, entities);
      runner.currentTick = (int)start;
      runner.runFor(100);

      Logger.LogEntities("After running", entities, runner.currentTick);
    }
  }
}