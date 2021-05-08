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
        var entities = Enumerable.Range(0, 10).Select(it => Person.Create()).ToList();

        entities.ForEach(entity => db.Entities.Add(entity));

        Logger.LogEntities(entities);

        var systems = new List<Systems.System>() {
          new Systems.AgeSystem()
        };

        for (var i = 0; i < 100; i++)
        {
          systems.ForEach(system =>
          {
            system.Update(1, entities);
          });
        }

        Logger.LogEntities(entities);

        db.SaveChanges();

        foreach (var entity in db.Entities.ToList())
        {
          Console.WriteLine($"Entity {entity.Name}");
        }
      }
    }
  }
}