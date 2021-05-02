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

    public static Entity Create()
    {
      return new Entity
      {
        Name = "Person",
        Components = {
          CreateName()
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

    private static Component CreateAge()
    {
      return new Component
      {
        Name = "Age",
        IntValues = {
          new IntValue
          {
            Name = "Days",
            Value = Random.random.Next(100 * 365)
          }
        }
      };
    }
  }
}