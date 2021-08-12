using System;
using System.Collections.Generic;

namespace EntityComponentSystem
{
    public partial class Ecs
    {
        public const int MAX_ENTITIES = 1000;
        private IdGenerator entityIds = new IdGenerator();
        private IdGenerator componentIds = new IdGenerator();
        private List<int> entities = new List<int>();

        public int CreateEntity()
        {
            var id = entityIds.GetNext();
            entities.Add(id);
            return id;
        }

        public IEnumerable<int> GetEntities()
        {
            return this.entities;
        }

        public void ForEachComponent(Action<int, string, object> action)
        {
            var componentKinds = components.Keys;
            foreach (var entityId in entities)
            {
                foreach (var componentKind in componentKinds)
                {
                    if (HasComponent(componentKind, entityId))
                    {
                        action(entityId, componentNames[componentKind], GetComponent(componentKind, entityId));
                    }
                }
            }
        }

        public void DestroyEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, object[]> components = new Dictionary<int, object[]>();

        private Dictionary<int, string> componentNames = new Dictionary<int, string>();

        public int RegisterComponentKind(string name)
        {
            var id = componentIds.GetNext();
            components.Add(id, new object[MAX_ENTITIES]);
            componentNames.Add(id, name);
            return id;
        }

        public object GetComponent(int componentKind, int entityId)
        {
            return components[componentKind][entityId];
        }
        public bool HasComponent(int componentKind, int entityId)
        {
            return components[componentKind][entityId] != null;
        }
        public void SetComponent(int componentKind, int entityId, object thing)
        {
            components[componentKind][entityId] = thing;
        }
        public void DeleteComponent(int componentKind, int entityId)
        {
            components[componentKind][entityId] = null;
        }
    }
}