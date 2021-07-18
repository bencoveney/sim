using System;
using System.Linq;
using Sim.Components;
using Sim.Ecs;
using Sim.Logging;
using Sim.Runner;
using Sim.Factories;

namespace Sim.Systems
{
    class DeathSystem : Ecs.System
    {
        private int deathAge = Ticks.From(80, 0, 0, 0, 0);
        private Schedule schedule;
        public DeathSystem(Schedule schedule)
        {
            this.schedule = schedule;
        }
        public void Update(EntityPool entityPool, int deltaTicks, int currentTick)
        {
            var entities = entityPool.AliveEntities;
            var count = entities.Count();
            for (int i = 0; i < count; i++)
            {
                var age = currentTick - entities[i].Get<BirthComponent>().Tick;

                if (age > deathAge)
                {
                    var target = entities[i];
                    Console.WriteLine($"{Describe.Entity(target)} was marked for death on {Ticks.ToDateString(currentTick)}");
                    schedule.Add24Hours(currentTick, (scheduledTick) =>
                    {
                        Console.WriteLine($"{Describe.Entity(target)} has died on {Ticks.ToDateString(scheduledTick)}");
                        target.Add(new DeathComponent(scheduledTick));
                        entityPool.UpdateFilters();
                    });
                }
            }
        }
    }
}