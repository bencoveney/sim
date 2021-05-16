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
          return Component(entity.Components[ComponentName.PersonName]);
        case EntityKind.Location:
          return Component(entity.Components[ComponentName.LocationName]);
        default:
          return "[COULD NOT DESCRIBE ENTITY]";
      }
    }

    public static string Component(Component component)
    {
      switch (component.Name)
      {
        case ComponentName.PersonName:
          return $"{Describe.GetStringValue(component, StringValueName.FirstName)} {Describe.GetStringValue(component, StringValueName.Surname)}";
        case ComponentName.Birth:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntValueName.Tick));
        case ComponentName.Death:
          return Ticks.ToDateString(Describe.GetIntValue(component, IntValueName.Tick));
        case ComponentName.LocationName:
          return Describe.GetStringValue(component, StringValueName.LocationName);
        case ComponentName.ParentLocation:
        case ComponentName.Position:
        case ComponentName.Home:
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