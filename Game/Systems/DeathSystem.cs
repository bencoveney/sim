using System;
using System.Linq;
using Sim.Components;
using EntityComponentSystem;
using Sim.Logging;
using Sim.Runner;
using EcsExtensions;

namespace Sim.Systems
{
    class DeathSystem : EntityComponentSystem.System
    {
        private readonly int deathAge = Ticks.From(80, 0, 0, 0, 0);
        private readonly Schedule schedule;
        public DeathSystem(Schedule schedule)
        {
            this.schedule = schedule;
        }
        public void Update(Ecs ecs, int deltaTicks, int currentTick)
        {
            var entities = ecs.GetEntities().Where(entity => ecs.HasBirth(entity) && !ecs.HasDeath(entity)).ToList();
            var count = entities.Count;
            for (int i = 0; i < count; i++)
            {
                var age = currentTick - ecs.GetBirth(entities[i]).Tick;

                if (age > deathAge)
                {
                    var target = entities[i];
                    Console.WriteLine($"{Describe.Entity(ecs, target)} was marked for death on {Ticks.ToDateString(currentTick)}");
                    schedule.Add24Hours(currentTick, (scheduledTick) =>
                    {
                        Console.WriteLine($"{Describe.Entity(ecs, target)} has died on {Ticks.ToDateString(scheduledTick)}");
                        ecs.SetDeath(target, new Death(scheduledTick));
                    });
                }
            }
        }
    }
}