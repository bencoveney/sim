using Sim.Components;
using Sim.Ecs;
using Sim.Utils;

namespace Sim.Factories
{
    class PersonFactory
    {
        public static Entity CreateElder(EntityPool entityPool, int ageRange)
        {
            var entity = entityPool.CreateEntity();
            entity.Add(new PersonNameComponent(Random.Name(), Random.Name()));
            entity.Add(new BirthComponent(Random.random.Next(ageRange)));
            return entity;
        }

        public static Entity CreateBaby(EntityPool entityPool, Entity parent1, Entity parent2, int currentTick)
        {
            var entity = entityPool.CreateEntity();
            entity.Add(new PersonNameComponent(Random.Name(), Random.Name()));
            entity.Add(new BirthComponent(currentTick));
            entity.Add(new HomeComponent(parent1.Get<HomeComponent>().EntityId));
            entity.Add(new PositionComponent(parent1.Get<PositionComponent>().EntityId));
            return entity;
        }
    }
}