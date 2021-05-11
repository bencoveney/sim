using System.Collections.Generic;
using Sim.Model;

namespace Sim.Systems
{
  class AgeSystem : System
  {
    public void Update(float deltaTime, List<Entity> entities)
    {
      // var relevant = entities.FindAll(entity => entity.Components.Exists(component => component.Name == "Age"));

      // foreach (Entity entity in relevant)
      // {
      //   var ageComponent = entity.Components.Find(component => component.Name == "Age");
      //   var daysValue = ageComponent.IntValues.Find(intValue => intValue.Name == "Days");
      //   // TODO: Datatypes (float and int) are a mess here.
      //   daysValue.Value += (int)deltaTime;
      // }
    }
  }
}