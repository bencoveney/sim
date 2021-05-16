using System;
using System.Collections.Generic;
using Sim.Ecs;

namespace Sim.Logging
{
  class Logger
  {
    public static void LogEntities(string header, IEnumerable<Entity> entities, float currentTick)
    {
      Console.WriteLine($"{Environment.NewLine}{header} ------");

      foreach (var entity in entities)
      {
        Console.WriteLine($"{Environment.NewLine}{entity.Name}");
        foreach (var component in entity.Components.Values)
        {
          switch (component.Name)
          {
            case ComponentName.PersonName:
              Console.WriteLine($"- Name: {Describe.Component(component)}");
              break;
            case ComponentName.Birth:
              Console.WriteLine($"- Born: {Describe.Component(component)}");
              break;
            case ComponentName.Death:
              Console.WriteLine($"- Died: {Describe.Component(component)}");
              break;
            case ComponentName.LocationName:
              Console.WriteLine($"- Name: {Describe.Component(component)}");
              break;
            case ComponentName.ParentLocation:
              Console.WriteLine($"- Is In: {Describe.Component(component)}");
              break;
            case ComponentName.Position:
              Console.WriteLine($"- Is In: {Describe.Component(component)}");
              break;
            case ComponentName.Home:
              Console.WriteLine($"- Home: {Describe.Component(component)}");
              break;
            default:
              Console.WriteLine($"- {component.Name}");
              foreach (var value in component.Bools.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.Floats.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.Ints.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.Strings.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.Entities.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              break;
          }
        }
      }
    }
  }
}
