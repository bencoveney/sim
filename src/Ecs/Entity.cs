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
        public Dictionary<int, Component> ComponentsByKind { get; } = new Dictionary<int, Component>();
        public void AddComponent(Component component)
        {
            this.ComponentsByKind.Add(component.Kind, component);
            Updated.EntityUpdated(this);
        }
    }
}