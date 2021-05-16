using Sim.Ecs;
using Sim.World;

namespace Sim.Logging
{
  class Describe
  {
    public static string Entity(Entity entity)
    {
      switch (entity.Kind.ToEntityKind())
      {
        case EntityKind.Person:
          return Component(entity.ComponentsByKind[ComponentKind.PersonName.ToInt()]);
        case EntityKind.Location:
          return Component(entity.ComponentsByKind[ComponentKind.LocationName.ToInt()]);
        default:
          return "[COULD NOT DESCRIBE ENTITY]";
      }
    }

    public static string Component(Component component)
    {
      switch (component.Kind.ToComponentKind())
      {
        case ComponentKind.PersonName:
          return $"{Describe.GetStringValue(component, StringValueName.FirstName)} {Describe.GetStringValue(component, StringValueName.Surname)}";
        case ComponentKind.Birth:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntValueName.Tick));
        case ComponentKind.Death:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntValueName.Tick));
        case ComponentKind.LocationName:
          return Describe.GetStringValue(component, StringValueName.LocationName);
        case ComponentKind.ParentLocation:
        case ComponentKind.Position:
        case ComponentKind.Home:
          return Entity(component.Entities[EntityValueName.Entity].Value);
        default:
          return "[COULD NOT DESCRIBE COMPONENT]";
      }
    }

    public static int GetIntValue(Component component, IntValueName name)
    {
      return component.Ints[name].Value;
    }

    public static string GetStringValue(Component component, StringValueName name)
    {
      return component.Strings[name].Value;
    }
  }
}