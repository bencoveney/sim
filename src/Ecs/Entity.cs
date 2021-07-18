using System.Collections.Generic;
using System.Linq;

namespace Sim.Ecs
{
    public class Entity
    {
        public Entity(uint id)
        {
            this.Id = id;
        }

        public uint Id { get; private set; }
        private List<Component> _components = new List<Component>();
        public IEnumerable<Component> Components { get => _components; }

        public T Get<T>() where T : Component
        {
            return _components.First(component => component.GetType() == typeof(T)) as T;
        }

        public bool Has<T>() where T : Component
        {
            return _components.Any(component => component.GetType() == typeof(T));
        }

        public void Add(Component component)
        {
            this._components.Add(component);
        }
    }
}