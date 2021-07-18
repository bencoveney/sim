using System.Collections.Generic;

namespace Ecs
{
    public class EntityPool
    {
        private SortedDictionary<uint, Entity> entities = new SortedDictionary<uint, Entity>();
        private IdGenerator entityIdGenerator = new IdGenerator();

        public Entity CreateEntity()
        {
            var id = entityIdGenerator.getNext();
            var entity = new Entity(id);
            this.entities.Add(id, entity);
            return entity;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return this.entities.Values;
        }
    }
}