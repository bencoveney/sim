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

      var towns = 3;
      var townPop = 10;
      var popsize = towns * townPop;

      WorldFactory.Create(entityPool, start, 3, townPop);

      Logger.LogEntities("Before running", entityPool.GetEntities(), start);

      var schedule = new Schedule();
      var runner = new Runner.Runner(entityPool, schedule);
      runner.AddSystem(new DeathSystem(schedule), Frequency.Day);
      runner.AddSystem(new BirthSystem(schedule, popsize), Frequency.Day);
      runner.currentTick = start;
      runner.runFor(100);

      Logger.LogEntities("After running", entityPool.GetEntities(), runner.currentTick);
    }
  }
}