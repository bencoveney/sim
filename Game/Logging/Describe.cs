using EntityComponentSystem;
using EcsExtensions;

namespace Sim.Logging
{
    class Describe
    {
        public static string Entity(Ecs ecs, int entity)
        {
            if (ecs.HasPersonName(entity))
            {
                return ecs.GetPersonName(entity).ToString();
            }
            else if (ecs.HasLocationName(entity))
            {
                return ecs.GetLocationName(entity).ToString();
            }
            else
            {
                return "[COULD NOT DESCRIBE ENTITY]";
            }
        }
    }
}