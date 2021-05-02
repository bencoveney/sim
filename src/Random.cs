using System;
using System.Collections.Generic;

namespace Sim
{
  class Random
  {
    public static System.Random random;

    static Random()
    {
      random = new System.Random((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
    }

    public static T Pick<T>(List<T> items)
    {
      return items[random.Next(items.Count)];
    }
  }
}