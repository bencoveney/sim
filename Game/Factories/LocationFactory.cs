using Sim.Components;
using EntityComponentSystem;
using EcsExtensions;

namespace Sim.Factories
{
    class LocationFactory
    {
        public static int CreateWorld(Ecs ecs, string name)
        {
            return CreateLocation(ecs, name);
        }
        public static int CreateBuilding(Ecs ecs, int parent, string name)
        {
            var entity = CreateLocation(ecs, name);
            ecs.SetParentLocation(entity, new ParentLocation(parent));
            return entity;
        }

        private static int CreateLocation(Ecs ecs, string name)
        {
            var entity = ecs.CreateEntity();
            ecs.SetLocationName(entity, new LocationName(name));
            return entity;
        }
    }
}