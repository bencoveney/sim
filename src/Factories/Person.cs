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
        Name = "Person",
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
        Name = "Name",
        StringValues = {
          new StringValue
          {
            Name = "FirstName",
            Value = Random.Pick(names)
          },
          new StringValue
          {
            Name = "Surname",
            Value = Random.Pick(names)
          },
        }
      };
    }

    private static Component CreateAge(int ageRange)
    {
      return new Component
      {
        Name = "Age",
        IntValues = {
          new IntValue
          {
            Name = "BirthTick",
            Value = Random.random.Next(ageRange)
          }
        }
      };
    }

    public static Component CreateDeath(int deathTick)
    {
      return new Component
      {
        Name = "Death",
        IntValues = {
          new IntValue
          {
            Name = "DeathTick",
            Value = deathTick
          }
        }
      };
    }
  }
}