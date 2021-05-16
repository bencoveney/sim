using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Factories
{
  class Person
  {
    private static List<string> names = new List<string>();

    static Person()
    {
      names.AddRange(Resource.Read("names.txt").Split(Environment.NewLine));
    }

    public static Entity Create(int ageRange)
    {
      return new Entity
      {
        Name = EntityName.Person,
        Components = {
          CreateName(),
          CreateAge(ageRange)
        }
      };
    }

    private static Component CreateName()
    {
      return new Component
      {
        Name = ComponentName.Name,
        StringValues = {
          new StringValue
          {
            Name = StringValueName.FirstName,
            Value = Random.Pick(names)
          },
          new StringValue
          {
            Name = StringValueName.Surname,
            Value = Random.Pick(names)
          },
        }
      };
    }

    private static Component CreateAge(int ageRange)
    {
      return new Component
      {
        Name = ComponentName.Birth,
        IntValues = {
          new IntValue
          {
            Name = IntValueName.Tick,
            Value = Random.random.Next(ageRange)
          }
        }
      };
    }

    public static Component CreateDeath(int deathTick)
    {
      return new Component
      {
        Name = ComponentName.Death,
        IntValues = {
          new IntValue
          {
            Name = IntValueName.Tick,
            Value = deathTick
          }
        }
      };
    }
  }
}