using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Systems
{
  class DeathSystem : System
  {
    public void Update(float deltaTime, float currentTick, List<Entity> entities)
    {
      var relevant = entities.FindAll(
        entity =>
          entity.Components.Exists(component => component.Name == ComponentName.Birth) &&
          !entity.Components.Exists(component => component.Name == ComponentName.Death));

      foreach (Entity entity in relevant)
      {

        var ageComponent = entity.Components.Find(component => component.Name == ComponentName.Birth);
        var birthTick = ageComponent.IntValues.Find(intValue => intValue.Name == IntValueName.Tick);

        var age = currentTick - birthTick.Value;

        var ageParts = Ticks.ToParts(age);

        if (ageParts.Years >= 80)
        {
          Console.WriteLine($"Death");
          entity.Components.Add(Factories.Person.CreateDeath((int)currentTick));
        }
      }
    }
  }
}