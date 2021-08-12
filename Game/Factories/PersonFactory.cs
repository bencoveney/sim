using Sim.Components;
using EntityComponentSystem;
using Sim.Utils;
using EcsExtensions;

namespace Sim.Factories
{
    class PersonFactory
    {
        public static int CreateElder(Ecs ecs, int ageRange)
        {
            var entity = ecs.CreateEntity();
            ecs.SetPersonName(entity, new PersonName(Random.Name(), Random.Name()));
            ecs.SetBirth(entity, new Birth(Random.random.Next(ageRange)));
            return entity;
        }

        public static int CreateBaby(Ecs ecs, int parent1, int parent2, int currentTick)
        {
            var entity = ecs.CreateEntity();
            ecs.SetPersonName(entity, new PersonName(Random.Name(), Random.Name()));
            ecs.SetBirth(entity, new Birth(currentTick));
            ecs.SetHome(entity, new Home(ecs.GetHome(parent1).EntityId));
            ecs.SetPosition(entity, new Position(ecs.GetPosition(parent1).EntityId));
            return entity;
        }
    }
}