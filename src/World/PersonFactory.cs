using Sim.Ecs;
using Sim.Utils;

namespace Sim.World
{
  class PersonFactory
  {
    public static Entity Create(EntityPool entityPool, int ageRange)
    {
      var entity = entityPool.Create(EntityKind.Person.ToInt());
      entity.AddComponent(CreateName());
      entity.AddComponent(CreateBirth(ageRange));
      return entity;
    }

    private static Component CreateName()
    {
      var component = new Component { Name = ComponentName.PersonName };
      component.AddString(new StringValue
      {
        Name = StringValueName.FirstName,
        Value = Random.Name()
      });
      component.AddString(new StringValue
      {
        Name = StringValueName.Surname,
        Value = Random.Name()
      });
      return component;
    }

    private static Component CreateBirth(int ageRange)
    {
      var component = new Component { Name = ComponentName.Birth };
      component.AddInt(new IntValue
      {
        Name = IntValueName.Tick,
        Value = Random.random.Next(ageRange)
      });
      return component;
    }

    public static Component CreateDeath(int deathTick)
    {
      var component = new Component { Name = ComponentName.Death };
      component.AddInt(new IntValue
      {
        Name = IntValueName.Tick,
        Value = deathTick
      });
      return component;
    }

    public static Component CreateHome(Entity position)
    {
      var component = new Component { Name = ComponentName.Home };
      component.AddEntity(new EntityValue
      {
        Name = EntityValueName.Entity,
        Value = position
      });
      return component;
    }

    public static Component CreatePosition(Entity position)
    {
      var component = new Component { Name = ComponentName.Position };
      component.AddEntity(new EntityValue
      {
        Name = EntityValueName.Entity,
        Value = position
      });
      return component;
    }
  }
}