using System.Collections.Generic;

namespace Sim.Ecs
{
  class EntityPool
  {
    private SortedDictionary<uint, Entity> entities = new SortedDictionary<uint, Entity>();
    private IdGenerator idGenerator = new IdGenerator();

    public Entity Create(int kind)
    {
      var id = idGenerator.getNext();
      var entity = new Entity(id, kind);
      this.entities.Add(id, entity);
      return entity;
    }

    public void Destroy(Entity entity)
    {
      this.Destroy(entity.Id);
    }

    public void Destroy(uint entityId)
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