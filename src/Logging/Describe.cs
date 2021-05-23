using Sim.Ecs;
using Sim.Runner;
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
          return $"{Describe.GetStringValue(component, StringKind.FirstName)} {Describe.GetStringValue(component, StringKind.Surname)}";
        case ComponentKind.Birth:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntKind.Tick));
        case ComponentKind.Death:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntKind.Tick));
        case ComponentKind.LocationName:
          return Describe.GetStringValue(component, StringKind.LocationName);
        case ComponentKind.ParentLocation:
        case ComponentKind.Position:
        case ComponentKind.Home:
          return Entity(component.Entities[EntityValueKind.Entity.ToInt()].Value);
        default:
          return "[COULD NOT DESCRIBE COMPONENT]";
      }
    }

    public static int GetIntValue(Component component, IntKind name)
    {
      return component.Ints[name.ToInt()].Value;
    }

    public static string GetStringValue(Component component, StringKind name)
    {
      return component.Strings[name.ToInt()].Value;
    }
  }
}