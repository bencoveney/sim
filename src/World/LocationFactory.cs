using Sim.Ecs;

namespace Sim.World
{
  class LocationFactory
  {
    public static Entity CreateWorld(EntityPool entityPool, string name)
    {
      return CreateLocation(entityPool, name);
    }
    public static Entity CreateBuilding(EntityPool entityPool, string name, Entity parent)
    {
      var entity = CreateLocation(entityPool, name);
      entity.AddComponent(CreateParent(parent));
      return entity;
    }

    private static Entity CreateLocation(EntityPool entityPool, string name)
    {
      var entity = entityPool.Create(EntityKind.Location.ToInt());
      entity.AddComponent(CreateName(name));
      return entity;
    }

    private static Component CreateName(string locationName)
    {
      var component = new Component { Name = ComponentName.LocationName };
      component.AddString(new StringValue
      {
        Name = StringValueName.LocationName,
        Value = locationName
      });
      return component;
    }

    private static Component CreateParent(Entity parent)
    {
      var component = new Component { Name = ComponentName.ParentLocation };
      component.AddEntity(new EntityValue
      {
        Name = EntityValueName.Entity,
        Value = parent
      });
      return component;
    }
  }
}