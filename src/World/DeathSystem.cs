using System;
using Sim.Ecs;
using Sim.Logging;

namespace Sim.World
{
  class DeathSystem : Ecs.System
  {
    private Filter filter = AliveFilter.Alive;
    public Filter GetFilter()
    {
      return this.filter;
    }
    public void Update(EntityPool entityPool, float deltaTime, float currentTick)
    {
      foreach (Entity entity in this.filter.GetEntities())
      {
        var age = currentTick - entity.ComponentsByKind[ComponentKind.Birth.ToInt()].Ints[IntKind.Tick.ToInt()].Value;

        if (Ticks.NumberOfYears(age) >= 80)
        {
          Console.WriteLine($"{Describe.Entity(entity)} has died on {Ticks.ToDateString(currentTick)}");
          PersonFactory.CreateDeath(entityPool.CreateBuilder(entity), (int)currentTick);
        }
      }
    }
  }
}