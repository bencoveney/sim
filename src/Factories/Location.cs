using Sim.Ecs;

namespace Sim.Factories
{
  class Location
  {
    public static Entity CreateWorld(string name)
    {
      return CreateLocation(name);
    }
    public static Entity CreateBuilding(string name, Entity parent)
    {
      var entity = CreateLocation(name);
      entity.AddComponent(CreateParent(parent));
      return entity;
    }

    private static Entity CreateLocation(string name)
    {
      var entity = new Entity { Name = EntityName.Location };
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