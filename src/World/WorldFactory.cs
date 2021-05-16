using System.Collections.Generic;
using System.Linq;
using Sim.Ecs;
using Sim.Logging;
using Sim.Utils;

namespace Sim.World
{
  class WorldFactory
  {
    public static IEnumerable<Entity> Create(EntityPool entityPool, int ageInTicks, int towns, int population)
    {
      var entities = new List<Entity>();

      var world = LocationFactory.CreateWorld(entityPool, "World");
      entities.Add(world);

      entities.AddRange(Enumerable.Range(0, towns).SelectMany(it => CreateTown(entityPool, ageInTicks, world, population)));

      return entities;
    }

    private static IEnumerable<Entity> CreateTown(EntityPool entityPool, int ageInTicks, Entity world, int population)
    {
      var entities = new List<Entity>();

      var town = LocationFactory.CreateBuilding(entityPool, $"{Random.Name()} Town", world);
      entities.Add(town);

      entities.AddRange(Enumerable.Range(0, population).SelectMany(it =>
      {
        var person = PersonFactory.Create(entityPool, ageInTicks);
        var home = LocationFactory.CreateBuilding(entityPool, $"{Describe.Entity(person)}'s Home", town);
        person.AddComponent(PersonFactory.CreatePosition(home));
        person.AddComponent(PersonFactory.CreateHome(home));
        return new List<Entity> { person, home };
      }));
      return entities;
    }
  }
}