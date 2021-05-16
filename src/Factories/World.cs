using System.Collections.Generic;
using System.Linq;
using Sim.Ecs;

namespace Sim.Factories
{
  class World
  {
    public static IEnumerable<Entity> Create(int ageInTicks, int towns, int population)
    {
      var entities = new List<Entity>();

      var world = Location.CreateWorld("World");
      entities.Add(world);

      entities.AddRange(Enumerable.Range(0, towns).SelectMany(it => CreateTown(ageInTicks, world, population)));

      return entities;
    }

    private static IEnumerable<Entity> CreateTown(int ageInTicks, Entity world, int population)
    {
      var entities = new List<Entity>();

      var town = Location.CreateBuilding($"{Random.Name()} Town", world);
      entities.Add(town);

      entities.AddRange(Enumerable.Range(0, population).SelectMany(it =>
      {
        var person = Person.Create(ageInTicks);
        var home = Location.CreateBuilding($"{Describe.Entity(person)}'s Home", town);
        person.AddComponent(Person.CreatePosition(home));
        person.AddComponent(Person.CreateHome(home));
        return new List<Entity> { person, home };
      }));
      return entities;
    }
  }
}