using System;
using System.Linq;
using EntityComponentSystem;
using Sim.Factories;
using EcsExtensions;
using Sim.Runner;
using Sim.Logging;

namespace Sim.Systems
{
    /*
      Current implementation is jank.
      If the population is below the goal popsize, then schedule a birth between two random people.
    */
    class BirthSystem : EntityComponentSystem.System
    {
        private readonly int popSize;
        public BirthSystem(int popSize)
        {
            this.popSize = popSize;
        }
        public void Update(Ecs ecs, int deltaTicks, int currentTick)
        {
            var entities = ecs.GetEntities().Where(entity => ecs.HasBirth(entity) && !ecs.HasDeath(entity)).ToList();
            if (entities.Count >= popSize)
            {
                return;
            }
            var parent1 = Utils.Random.Pick(entities);
            var parent2 = Utils.Random.Pick(entities);
            while (parent1 == parent2)
            {
                parent2 = Utils.Random.Pick(entities);
            }
            int baby = PersonFactory.CreateBaby(ecs, parent1, parent2, currentTick);
            Console.WriteLine($"{Describe.Entity(ecs, baby)} born to {Describe.Entity(ecs, parent1)} and {Describe.Entity(ecs, parent2)} on {Ticks.ToDateString(currentTick)}");
        }
    }
}