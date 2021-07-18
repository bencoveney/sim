using Sim.Components;
using Sim.Ecs;

namespace Sim.Logging
{
    class Describe
    {
        public static string Entity(Entity entity)
        {
            if (entity.Has<PersonNameComponent>())
            {
                return Component(entity.Get<PersonNameComponent>());
            }
            else if (entity.Has<LocationNameComponent>())
            {
                return Component(entity.Get<LocationNameComponent>());
            }
            else
            {
                return "[COULD NOT DESCRIBE ENTITY]";
            }
        }

        public static string Component(Component component)
        {
            return component.ToString();
        }
    }
}