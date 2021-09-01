using System;
using EntityComponentSystem;

namespace Sim.Logging
{
    class Logger
    {
        public static void LogEntities(string header, Ecs ecs)
        {
            Console.WriteLine($"{header} ------");
            ecs.ForEachComponent((entityId, componentName, component) =>
            {
                Console.WriteLine($"{entityId}: {componentName}: {component}");
            });
        }
    }
}