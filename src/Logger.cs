using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim
{
  class Logger
  {
    public static void LogEntities(IEnumerable<Entity> entities)
    {
      foreach (var entity in entities)
      {
        Console.WriteLine($"{entity.Name}");
        foreach (var component in entity.Components)
        {
          Console.WriteLine($"- {component.Name}");
          foreach (var value in component.BoolValues)
          {
            Console.WriteLine($"  - {value.Name}: {value.Value}");
          }
          foreach (var value in component.FloatValues)
          {
            Console.WriteLine($"  - {value.Name}: {value.Value}");
          }
          foreach (var value in component.IntValues)
          {
            Console.WriteLine($"  - {value.Name}: {value.Value}");
          }
          foreach (var value in component.StringValues)
          {
            Console.WriteLine($"  - {value.Name}: {value.Value}");
          }
        }
      }
    }
  }
}
