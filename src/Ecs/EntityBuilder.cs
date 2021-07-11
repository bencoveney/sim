namespace Sim.Ecs
{
    class EntityBuilder
    {
        private Entity entity;
        private EntityPool entityPool;
        public EntityBuilder(EntityPool entityPool, Entity entity)
        {
            this.entity = entity;
            this.entityPool = entityPool;
        }

        public Entity Entity { get => this.entity; }

        public Component AddComponent(int componentKind)
        {
            return this.entityPool.AddComponent(entity, componentKind);
        }
    }
}