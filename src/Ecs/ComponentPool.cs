using System.Collections.Generic;

namespace Sim.Ecs
{
    class ComponentPool
    {
        private SortedDictionary<uint, Component> components = new SortedDictionary<uint, Component>();
        private IdGenerator idGenerator = new IdGenerator();

        public Component CreateComponent(int componentKind)
        {
            var id = idGenerator.getNext();
            var component = new Component(id, componentKind);
            this.components.Add(id, component);
            return component;
        }
    }
}