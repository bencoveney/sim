using System;

namespace Sim.Runner
{
    static class ScheduleExtensions
    {
        public static void Add24Hours(this Schedule schedule, int currentTick, Action<int> action)
        {
            var tick = Utils.Random.random.Next(1, Ticks.PerDay);
            schedule.Add(currentTick + tick, action);
        }
    }
}