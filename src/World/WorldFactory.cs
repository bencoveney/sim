using System.Collections.Generic;
using System.Linq;
using Sim.Ecs;
using Sim.Logging;
using Sim.Utils;

namespace Sim.World
{
    class WorldFactory
    {
        public static void Create(EntityPool entityPool, int ageInTicks, int towns, int population)
        {
            var world = LocationFactory.CreateWorld(entityPool, "World").Entity;
            Range.To(towns).ForEach(it => CreateTown(entityPool, world, ageInTicks, population));
        }

        private static void CreateTown(EntityPool entityPool, Entity world, int ageInTicks, int population)
        {
            var town = LocationFactory.CreateBuilding(entityPool, world, $"{Random.Name()} Town").Entity;
            Range.To(population).ForEach(it => CreateInhabitant(entityPool, town, ageInTicks));
        }

        private static void CreateInhabitant(EntityPool entityPool, Entity town, int ageInTicks)
        {
            var personBuilder = PersonFactory.CreateElder(entityPool, ageInTicks);
            var home = LocationFactory.CreateBuilding(entityPool, town, $"{Describe.Entity(personBuilder.Entity)}'s Home").Entity;
            PersonFactory.CreatePosition(personBuilder, home);
            PersonFactory.CreateHome(personBuilder, home);
        }
    }
}