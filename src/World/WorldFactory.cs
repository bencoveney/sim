using System.Collections.Generic;
using System.Linq;
using Sim.Ecs;

namespace Sim.World
{
  class WorldFactory
  {
    public static IEnumerable<Entity> Create(int ageInTicks, int towns, int population)
    {
      var entities = new List<Entity>();

      var world = LocationFactory.CreateWorld("World");
      entities.Add(world);

      entities.AddRange(Enumerable.Range(0, towns).SelectMany(it => CreateTown(ageInTicks, world, population)));

      return entities;
    }

    private static IEnumerable<Entity> CreateTown(int ageInTicks, Entity world, int population)
    {
      var entities = new List<Entity>();

      var town = LocationFactory.CreateBuilding($"{Random.Name()} Town", world);
      entities.Add(town);

      entities.AddRange(Enumerable.Range(0, population).SelectMany(it =>
      {
        var person = PersonFactory.Create(ageInTicks);
        var home = LocationFactory.CreateBuilding($"{Describe.Entity(person)}'s Home", town);
        person.AddComponent(PersonFactory.CreatePosition(home));
        person.AddComponent(PersonFactory.CreateHome(home));
        return new List<Entity> { person, home };
      }));
      return entities;
    }
  }
}