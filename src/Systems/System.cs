using System.Collections.Generic;
using Sim.Model;

namespace Sim.Systems
{
  interface System
  {
    void Update(float deltaMinutes, List<Entity> entities);
  }
}