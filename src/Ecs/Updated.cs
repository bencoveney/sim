using System.Collections.Generic;

namespace Sim.Ecs
{
    class Updated
    {
        private static List<Entity> updated = new List<Entity>();
        public static void EntityUpdated(Entity entity)
        {
            updated.Add(entity);
        }
        public static void Clear()
        {
            updated.Clear();
        }
        public static IEnumerable<Entity> Get()
        {
            return updated;
        }
    }
}