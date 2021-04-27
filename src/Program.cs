using System;
using System.Linq;

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
        db.Entities.Add(new Entity() { Name = "Test" });

        db.SaveChanges();

        foreach (var entity in db.Entities.ToList())
        {
          Console.WriteLine($"Entity {entity.Name}");
        }
      }
    }
  }
}