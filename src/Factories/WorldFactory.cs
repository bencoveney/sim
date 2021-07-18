using Sim.Components;
using Sim.Ecs;
using Sim.Logging;
using Sim.Utils;

namespace Sim.Factories
{
    class WorldFactory
    {
        public static void Create(EntityPool entityPool, int ageInTicks, int towns, int population)
        {
            var world = LocationFactory.CreateWorld(entityPool, "World");
            Range.To(towns).ForEach(it => CreateTown(entityPool, world, ageInTicks, population));
        }

        private static void CreateTown(EntityPool entityPool, Entity world, int ageInTicks, int population)
        {
            var town = LocationFactory.CreateBuilding(entityPool, world, $"{Random.Name()} Town");
            Range.To(population).ForEach(it => CreateInhabitant(entityPool, town, ageInTicks));
        }

        private static void CreateInhabitant(EntityPool entityPool, Entity town, int ageInTicks)
        {
            var elder = PersonFactory.CreateElder(entityPool, ageInTicks);
            var home = LocationFactory.CreateBuilding(entityPool, town, $"{Describe.Entity(elder)}'s Home");
            elder.Add(new PositionComponent(home.Id));
            elder.Add(new HomeComponent(home.Id));
        }
    }
}