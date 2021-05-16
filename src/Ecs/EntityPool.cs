using System.Collections.Generic;

namespace Sim.Ecs
{
  class EntityPool
  {
    private SortedDictionary<uint, Entity> entities = new SortedDictionary<uint, Entity>();
    private IdGenerator entityIdGenerator = new IdGenerator();
    private ComponentPool componentPool = new ComponentPool();

    public Entity CreateEntity(int entityKind)
    {
      var id = entityIdGenerator.getNext();
      var entity = new Entity(id, entityKind);
      this.entities.Add(id, entity);
      return entity;
    }

    public EntityBuilder CreateBuilder(int entityKind)
    {
      return CreateBuilder(CreateEntity(entityKind));
    }

    public EntityBuilder CreateBuilder(Entity entity)
    {
      return new EntityBuilder(this, entity);
    }

    public void DestroyEntity(Entity entity)
    {
      this.DestroyEntity(entity.Id);
    }

    public void DestroyEntity(uint entityId)
    {
      this.entities.Remove(entityId);
    }

    public Component AddComponent(uint entityId, int componentKind)
    {
      return AddComponent(GetEntity(entityId), componentKind);
    }

    public Component AddComponent(Entity entity, int componentKind)
    {
      if (entity.ComponentsByKind.ContainsKey(componentKind))
      {
        throw new EcsException($"Entity ${entity.Id} already has component ${componentKind}");
      }

      var component = componentPool.CreateComponent(componentKind);
      entity.AddComponent(component);
      Updated.EntityUpdated(entity);
      return component;
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