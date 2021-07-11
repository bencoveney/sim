using System;
using System.Linq;
using Sim.Ecs;
using Sim.Logging;
using Sim.Runner;

namespace Sim.World
{
    /*
      Current implementation is jank.
      If the population is below the goal popsize, then schedule a birth between two random people.
    */
    class BirthSystem : Ecs.System
    {
        private Filter filter = AliveFilter.Alive;
        private Schedule schedule;
        private int popSize;
        public BirthSystem(Schedule schedule, int popSize)
        {
            this.schedule = schedule;
            this.popSize = popSize;
        }
        public Filter GetFilter()
        {
            return this.filter;
        }
        public void Update(EntityPool entityPool, int deltaTicks, int currentTick)
        {
            var entities = this.filter.GetEntities();
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
            Console.WriteLine($"${Environment.NewLine}Baby born");
        }
    }
}