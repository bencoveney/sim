using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim
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
            case ComponentName.Name:
              Console.WriteLine($"- Name: {GetStringValue(component, StringValueName.FirstName)} {GetStringValue(component, StringValueName.Surname)}");
              break;
            case ComponentName.Birth:
              var birthTick = GetIntValue(component, IntValueName.Tick);
              Console.WriteLine($"- Born: {Ticks.ToDateString(birthTick)}");
              break;
            case ComponentName.Death:
              var deathTick = GetIntValue(component, IntValueName.Tick);
              Console.WriteLine($"- Died: {Ticks.ToDateString(deathTick)}");
              break;
            default:
              Console.WriteLine($"- {component.Name}");
              foreach (var value in component.BoolValues.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.FloatValues.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.IntValues.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              foreach (var value in component.StringValues.Values)
              {
                Console.WriteLine($"  - {value.Name}: {value.Value}");
              }
              break;
          }
        }
      }
    }

    private static int GetIntValue(Component component, IntValueName name)
    {
      return component.IntValues[name].Value;
    }

    private static string GetStringValue(Component component, StringValueName name)
    {
      return component.StringValues[name].Value;
    }
  }
}
