using System.Collections.Generic;

namespace Sim.Ecs
{
  public enum EntityName
  {
    None = 0,
    Person = 1,
    Location = 2
  }

  public class Entity
  {
    public Entity(uint id)
    {
      this.Id = id;
    }

    public uint Id { get; set; }
    public EntityName Name { get; set; }
    public Dictionary<ComponentName, Component> Components { get; } = new Dictionary<ComponentName, Component>();
    public void AddComponent(Component component)
    {
      this.Components.Add(component.Name, component);
      component.EntityId = this.Id;
      Updated.EntityUpdated(this);
    }
  }
}