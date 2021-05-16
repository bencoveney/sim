using System.Collections.Generic;
using Sim.Filters;
using Sim.Model;

namespace Sim.Systems
{
  interface System
  {
    Filter GetFilter();
    void Update(float deltaMinutes, float totalMinutes);
  }
}