using System;
using System.Collections.Generic;
using Ecs;

namespace Sim.Logging
{
    class Logger
    {
        public static void LogEntities(string header, IEnumerable<Entity> entities, float currentTick)
        {
            Console.WriteLine($"{Environment.NewLine}{header} ------");

            foreach (var entity in entities)
            {
                Console.WriteLine($"{Environment.NewLine}Entity");
                foreach (var component in entity.Components)
                {
                    var componentName = component.GetType().ToString();
                    Console.WriteLine($"- {componentName}: {component}");
                }
            }
        }
    }
}