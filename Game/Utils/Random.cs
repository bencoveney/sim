using System;
using System.Collections.Generic;

namespace Sim.Utils
{
    class Random
    {
        private static List<string> names = new List<string>();
        public static System.Random random;

        static Random()
        {
            random = new System.Random((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            names.AddRange(Resource.ReadString("names.txt", Resource.Kind.Data).Split(Environment.NewLine));
        }

        public static T Pick<T>(IList<T> items)
        {
            return items[random.Next(items.Count)];
        }

        public static string Name()
        {
            return Pick(names);
        }
    }
}