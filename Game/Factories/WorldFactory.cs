using Sim.Components;
using EntityComponentSystem;
using Sim.Logging;
using Sim.Utils;
using EcsExtensions;

namespace Sim.Factories
{
    class WorldFactory
    {
        public static void Create(Ecs ecs, int ageInTicks, int towns, int population)
        {
            var world = LocationFactory.CreateWorld(ecs, "World");
            Range.To(towns).ForEach(it => CreateTown(ecs, world, ageInTicks, population));
        }

        private static void CreateTown(Ecs ecs, int world, int ageInTicks, int population)
        {
            var town = LocationFactory.CreateBuilding(ecs, world, $"{Random.Name()} Town");
            Range.To(population).ForEach(it => CreateInhabitant(ecs, town, ageInTicks));
        }

        private static void CreateInhabitant(Ecs ecs, int town, int ageInTicks)
        {
            var elder = PersonFactory.CreateElder(ecs, ageInTicks);
            var home = LocationFactory.CreateBuilding(ecs, town, $"{Describe.Entity(ecs, elder)}'s Home");
            ecs.SetPosition(elder, new Position(home));
            ecs.SetHome(elder, new Home(home));
        }
    }
}