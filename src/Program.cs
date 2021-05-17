﻿using System.Collections.Generic;
using Sim.Ecs;
using Sim.Logging;
using Sim.World;

namespace Sim
{
  internal class Program
  {
    private static void Main()
    {
      var start = Ticks.From(50, 0, 0, 0, 0);

      var entityPool = new EntityPool();

      WorldFactory.Create(entityPool, start, 3, 10);

      Logger.LogEntities("Before running", entityPool.GetEntities(), start);

      var systems = new List<Ecs.System>()
      {
        new DeathSystem(),
        new PositionSystem()
      };

      var runner = new Runner(entityPool, systems);
      runner.currentTick = start;
      runner.runFor(100);

      Logger.LogEntities("After running", entityPool.GetEntities(), runner.currentTick);
    }
  }
}