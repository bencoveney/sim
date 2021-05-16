using System;
using System.Collections.Generic;
using System.Linq;
using Sim.Factories;
using Sim.Model;

namespace Sim
{
  internal class Program
  {
    private static void Main()
    {

      var dbLocation = DbLocation.Get();
      Console.WriteLine($"DB Location: {dbLocation}");
      using (var db = new SimContext(dbLocation))
      {
        var start = Ticks.From(50, 0, 0, 0, 0);

        var entities = Enumerable.Range(0, 10).Select(it => Person.Create((int)start)).ToList();

        entities.ForEach(entity => db.Entities.Add(entity));

        Logger.LogEntities("Before running", entities, start);

        var systems = new List<Systems.System>()
        {
          new Systems.DeathSystem()
        };

        var runner = new Runner(systems, entities);
        runner.currentTick = (int)start;
        runner.runFor(100);

        Logger.LogEntities("After running", entities, runner.currentTick);

        db.SaveChanges();
      }
    }
  }
}