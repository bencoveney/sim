using Sim.Components;
using Sim.Ecs;

namespace Sim.Factories
{
    class LocationFactory
    {
        public static Entity CreateWorld(EntityPool entityPool, string name)
        {
            return CreateLocation(entityPool, name);
        }
        public static Entity CreateBuilding(EntityPool entityPool, Entity parent, string name)
        {
            var entity = entityPool.CreateEntity();
            entity.Add(new ParentLocationComponent(parent.Id));
            return entity;
        }

        private static Entity CreateLocation(EntityPool entityPool, string name)
        {
            var entity = entityPool.CreateEntity();
            entity.Add(new LocationNameComponent(name));
            return entity;
        }
    }
}