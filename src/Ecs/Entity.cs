using System.Collections.Generic;

namespace Sim.Ecs
{
  public class Entity
  {
    public Entity(uint id, int kind)
    {
      this.Id = id;
      this.Kind = kind;
    }

    public uint Id { get; private set; }
    public int Kind { get; private set; }
    public Dictionary<ComponentName, Component> Components { get; } = new Dictionary<ComponentName, Component>();
    public void AddComponent(Component component)
    {
      this.Components.Add(component.Name, component);
      component.EntityId = this.Id;
      Updated.EntityUpdated(this);
    }
  }
}