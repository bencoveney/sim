using System;
using System.Collections.Generic;
using Sim.Model;

namespace Sim.Factories
{
  class Person
  {
    static Person()
    {
    }

    public static Entity Create(int ageRange)
    {
      var entity = new Entity { Name = EntityName.Person };
      entity.AddComponent(CreateName());
      entity.AddComponent(CreateBirth(ageRange));
      return entity;
    }

    private static Component CreateName()
    {
      var component = new Component { Name = ComponentName.PersonName };
      component.AddString(new StringValue
      {
        Name = StringValueName.FirstName,
        Value = Random.Name()
      });
      component.AddString(new StringValue
      {
        Name = StringValueName.Surname,
        Value = Random.Name()
      });
      return component;
    }

    private static Component CreateBirth(int ageRange)
    {
      var component = new Component { Name = ComponentName.Birth };
      component.AddInt(new IntValue
      {
        Name = IntValueName.Tick,
        Value = Random.random.Next(ageRange)
      });
      return component;
    }

    public static Component CreateDeath(int deathTick)
    {
      var component = new Component { Name = ComponentName.Death };
      component.AddInt(new IntValue
      {
        Name = IntValueName.Tick,
        Value = deathTick
      });
      return component;
    }
  }
}