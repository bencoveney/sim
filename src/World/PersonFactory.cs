using Sim.Ecs;
using Sim.Utils;

namespace Sim.World
{
  class PersonFactory
  {
    public static EntityBuilder Create(EntityPool entityPool, int ageRange)
    {
      var builder = entityPool.CreateBuilder(EntityKind.Person.ToInt());
      CreateName(builder);
      CreateBirth(builder, ageRange);
      return builder;
    }

    private static Component CreateName(EntityBuilder builder)
    {
      var component = builder.AddComponent(ComponentKind.PersonName.ToInt());
      component.AddString(new StringValue(StringKind.FirstName.ToInt(), Random.Name()));
      component.AddString(new StringValue(StringKind.Surname.ToInt(), Random.Name()));
      return component;
    }

    private static Component CreateBirth(EntityBuilder builder, int ageRange)
    {
      var component = builder.AddComponent(ComponentKind.Birth.ToInt());
      component.AddInt(new IntValue(IntKind.Tick.ToInt(), Random.random.Next(ageRange)));
      return component;
    }

    public static Component CreateDeath(EntityBuilder builder, int deathTick)
    {
      var component = builder.AddComponent(ComponentKind.Death.ToInt());
      component.AddInt(new IntValue(IntKind.Tick.ToInt(), deathTick));
      return component;
    }

    public static Component CreateHome(EntityBuilder builder, Entity position)
    {
      var component = builder.AddComponent(ComponentKind.Home.ToInt());
      component.AddEntity(new EntityValue(EntityValueKind.Entity.ToInt(), position));
      return component;
    }

    public static Component CreatePosition(EntityBuilder builder, Entity position)
    {
      var component = builder.AddComponent(ComponentKind.Position.ToInt());
      component.AddEntity(new EntityValue(EntityValueKind.Entity.ToInt(), position));
      return component;
    }
  }
}