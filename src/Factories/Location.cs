using Sim.Model;

namespace Sim.Factories
{
  class Location
  {
    public static Entity Create(string name)
    {
      var entity = new Entity { Name = EntityName.Location };
      entity.AddComponent(CreateName(name));
      return entity;
    }
    public static Entity Create(string name, Entity parent)
    {
      var entity = Create(name);
      entity.AddComponent(CreateParent(parent));
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