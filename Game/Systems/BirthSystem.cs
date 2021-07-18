using System;
using System.Linq;
using Ecs;
using Sim.Runner;
using Sim.Factories;
using Sim.Components;

namespace Sim.Systems
{
    /*
      Current implementation is jank.
      If the population is below the goal popsize, then schedule a birth between two random people.
    */
    class BirthSystem : Ecs.System
    {
        private Schedule schedule;
        private int popSize;
        public BirthSystem(Schedule schedule, int popSize)
        {
            this.schedule = schedule;
            this.popSize = popSize;
        }
        public void Update(EntityPool entityPool, int deltaTicks, int currentTick)
        {
            var entities = entityPool.GetEntities().Where(entity => entity.Has<BirthComponent>() && !entity.Has<DeathComponent>()).ToList();
            if (entities.Count() >= popSize)
            {
                return;
            }
            var parent1 = Utils.Random.Pick<Entity>(entities);
            var parent2 = Utils.Random.Pick<Entity>(entities);
            while (parent1 == parent2)
            {
                parent2 = Utils.Random.Pick<Entity>(entities);
            }
            PersonFactory.CreateBaby(entityPool, parent1, parent2, currentTick);
            Console.WriteLine($"{Environment.NewLine}Baby born");
        }
    }
}