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
          [ComponentName.Name] = CreateName(),
          [ComponentName.Birth] = CreateBirth(ageRange)
        }
      };
    }

    private static Component CreateName()
    {
      return new Component
      {
        Name = ComponentName.Name,
        StringValues = {
          [StringValueName.FirstName] = new StringValue
          {
            Name = StringValueName.FirstName,
            Value = Random.Pick(names)
          },
          [StringValueName.Surname] = new StringValue
          {
            Name = StringValueName.Surname,
            Value = Random.Pick(names)
          },
        }
      };
    }

    private static Component CreateBirth(int ageRange)
    {
      return new Component
      {
        Name = ComponentName.Birth,
        IntValues = {
          [IntValueName.Tick] = new IntValue
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
          [IntValueName.Tick] = new IntValue
          {
            Name = IntValueName.Tick,
            Value = deathTick
          }
        }
      };
    }
  }
}