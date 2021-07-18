using System.Collections.Generic;
using System.Linq;
using Sim.Components;

namespace Sim.Ecs
{
    class EntityPool
    {
        private SortedDictionary<uint, Entity> entities = new SortedDictionary<uint, Entity>();
        private IdGenerator entityIdGenerator = new IdGenerator();
        public List<Entity> AliveEntities = new List<Entity>();

        public void UpdateFilters()
        {
            // Hack!!!
            // Keep an updated list of entities that are alive so that each system doesn't need to recalculate this.
            // Should be improved by building a filtering system.
            AliveEntities = GetEntities().Where(entity => entity.Has<BirthComponent>() && !entity.Has<DeathComponent>()).ToList();
        }

        public Entity CreateEntity()
        {
            var id = entityIdGenerator.getNext();
            var entity = new Entity(id);
            this.entities.Add(id, entity);
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            this.DestroyEntity(entity.Id);
        }

        public void DestroyEntity(uint entityId)
        {
            this.entities.Remove(entityId);
        }

        public IEnumerable<Entity> GetEntities()
        {
            return this.entities.Values;
        }

        public Entity GetEntity(uint entityId)
        {
            return this.entities[entityId];
        }
    }
}