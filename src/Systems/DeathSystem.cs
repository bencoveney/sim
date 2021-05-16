using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Systems
{
  class DeathSystem : System
  {
    public void Update(float deltaTime, float currentTick, List<Entity> entities)
    {
      foreach (Entity entity in entities)
      {
        if (!entity.Components.ContainsKey(ComponentName.Birth))
        {
          continue;
        }

        if (entity.Components.ContainsKey(ComponentName.Death))
        {
          continue;
        }

        var ageComponent = entity.Components[ComponentName.Birth];
        var birthTick = entity.Components[ComponentName.Birth].IntValues[IntValueName.Tick];

        var age = currentTick - birthTick.Value;

        if (Ticks.NumberOfYears(age) >= 80)
        {
          Console.WriteLine($"Death");
          entity.Components.Add(ComponentName.Death, Factories.Person.CreateDeath((int)currentTick));
        }
      }
    }
  }
}