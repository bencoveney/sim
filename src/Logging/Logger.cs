using System;
using System.Collections.Generic;
using Sim.Ecs;
using Sim.World;

namespace Sim.Logging
{
  class Logger
  {
    public static void LogEntities(string header, IEnumerable<Entity> entities, float currentTick)
    {
      Console.WriteLine($"{Environment.NewLine}{header} ------");

      foreach (var entity in entities)
      {
        Console.WriteLine($"{Environment.NewLine}{entity.Kind.ToEntityKind()}");
        foreach (var component in entity.ComponentsByKind.Values)
        {
          switch (component.Kind.ToComponentKind())
          {
            case ComponentKind.PersonName:
              Console.WriteLine($"- Name: {Describe.Component(component)}");
              break;
            case ComponentKind.Birth:
              Console.WriteLine($"- Born: {Describe.Component(component)}");
              break;
            case ComponentKind.Death:
              Console.WriteLine($"- Died: {Describe.Component(component)}");
              break;
            case ComponentKind.LocationName:
              Console.WriteLine($"- Name: {Describe.Component(component)}");
              break;
            case ComponentKind.ParentLocation:
              Console.WriteLine($"- Is In: {Describe.Component(component)}");
              break;
            case ComponentKind.Position:
              Console.WriteLine($"- Is In: {Describe.Component(component)}");
              break;
            case ComponentKind.Home:
              Console.WriteLine($"- Home: {Describe.Component(component)}");
              break;
            default:
              Console.WriteLine($"- {component.Kind.ToComponentKind()}");
              foreach (var value in component.Ints.Values)
              {
                Console.WriteLine($"  - {value.Kind.ToIntKind()}: {value.Value}");
              }
              foreach (var value in component.Strings.Values)
              {
                Console.WriteLine($"  - {value.Kind.ToStringKind()}: {value.Value}");
              }
              foreach (var value in component.Entities.Values)
              {
                Console.WriteLine($"  - {value.Kind.ToEntityKind()}: {value.Value}");
              }
              break;
          }
        }
      }
    }
  }
}
