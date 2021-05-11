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
        foreach (var component in entity.Components)
        {
          switch (component.Name)
          {
            case "Name":
              Console.WriteLine($"- Name: {GetStringValue(component, "FirstName")} {GetStringValue(component, "Surname")}");
              break;
            case "Age":
              var birthTick = GetIntValue(component, "BirthTick");
              var ageTicks = currentTick - birthTick;
              Console.WriteLine($"- Age: {Ticks.ToAbsString(ageTicks)}");
              break;
            default:
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
              break;
          }
        }
      }
    }

    private static int GetIntValue(Component component, string name)
    {
      return component.IntValues.Find(value => value.Name == name).Value;
    }

    private static string GetStringValue(Component component, string name)
    {
      return component.StringValues.Find(value => value.Name == name).Value;
    }
  }
}
