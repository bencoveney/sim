using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Systems
{
  class DeathSystem : System
  {
    private Filter filter = new Filter(
      new List<ComponentName> { ComponentName.Birth },
      new List<ComponentName> { ComponentName.Death }
    );
    public Filter GetFilter()
    {
      return this.filter;
    }
    public void Update(float deltaTime, float currentTick)
    {
      foreach (Entity entity in this.filter.GetEntities())
      {
        var age = currentTick - entity.Components[ComponentName.Birth].Ints[IntValueName.Tick].Value;

        if (Ticks.NumberOfYears(age) >= 80)
        {
          Console.WriteLine($"Death");
          entity.AddComponent(Factories.Person.CreateDeath((int)currentTick));
        }
      }
    }
  }
}