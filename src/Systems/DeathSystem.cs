using System;
using Sim.Ecs;
using Sim.Filters;

namespace Sim.Systems
{
  class DeathSystem : Ecs.System
  {
    private Filter filter = AliveFilter.Alive;
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
          Console.WriteLine($"{Describe.Entity(entity)} has died on {Ticks.ToDateString(currentTick)}");
          entity.AddComponent(Factories.Person.CreateDeath((int)currentTick));
        }
      }
    }
  }
}