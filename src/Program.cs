using System;
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
        foreach (int value in Enumerable.Range(0, 10))
        {
          db.Entities.Add(Person.Create());
        }

        db.SaveChanges();

        foreach (var entity in db.Entities.ToList())
        {
          Console.WriteLine($"Entity {entity.Name}");
        }
      }
    }
  }
}