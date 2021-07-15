using Sim.Ecs;

namespace Sim.World
{
    class LocationFactory
    {
        public static EntityBuilder CreateWorld(EntityPool entityPool, string name)
        {
            return CreateLocation(entityPool, name);
        }
        public static EntityBuilder CreateBuilding(EntityPool entityPool, Entity parent, string name)
        {
            var builder = CreateLocation(entityPool, name);
            CreateParent(builder, parent);
            return builder;
        }

        private static EntityBuilder CreateLocation(EntityPool entityPool, string name)
        {
            var builder = entityPool.CreateBuilder();
            CreateName(builder, name);
            return builder;
        }

        private static Component CreateName(EntityBuilder builder, string locationName)
        {
            var component = builder.AddComponent(ComponentKind.LocationName.ToInt());
            component.AddString(new StringValue(StringKind.LocationName.ToInt(), locationName));
            return component;
        }

        private static Component CreateParent(EntityBuilder builder, Entity parent)
        {
            var component = builder.AddComponent(ComponentKind.ParentLocation.ToInt());
            component.AddEntity(new EntityValue(EntityValueKind.Entity.ToInt(), parent));
            return component;
        }
    }
}