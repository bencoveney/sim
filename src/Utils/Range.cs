using System.Collections.Generic;
using System.Linq;

namespace Sim.Utils
{
  static class Range
  {
    public static List<int> To(int n)
    {
      return FromTo(0, n);
    }
    public static List<int> FromTo(int from, int to)
    {
      return Enumerable.Range(from, to).ToList();
    }
  }
}