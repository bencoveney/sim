using System;
using System.Linq;
using Sim.Ecs;
using Sim.Logging;
using Sim.Runner;

namespace Sim.World
{
    class DeathSystem : Ecs.System
    {
        private Filter filter = AliveFilter.Alive;
        private int deathAge = Ticks.From(80, 0, 0, 0, 0);
        private Schedule schedule;
        public DeathSystem(Schedule schedule)
        {
            this.schedule = schedule;
        }
        public Filter GetFilter()
        {
            return this.filter;
        }
        public void Update(EntityPool entityPool, int deltaTicks, int currentTick)
        {
            var entities = this.filter.GetEntities();
            var count = entities.Count();
            for (int i = 0; i < count; i++)
            {
                var age = currentTick - entities[i].ComponentsByKind[2].Ints[1].Value;

                if (age > deathAge)
                {
                    var target = entities[i];
                    Console.WriteLine($"{Describe.Entity(target)} was marked for death on {Ticks.ToDateString(currentTick)}");
                    schedule.Add24Hours(currentTick, (scheduledTick) =>
                    {
                        Console.WriteLine($"{Describe.Entity(target)} has died on {Ticks.ToDateString(scheduledTick)}");
                        PersonFactory.CreateDeath(entityPool.CreateBuilder(target), scheduledTick);
                    });
                }
            }
        }
    }
}