using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EntityComponentSystem;

namespace Sim.Runner
{
    class Runner
    {
        private Ecs ecs;
        private Dictionary<Frequency, List<TimedSystem>> systems = new Dictionary<Frequency, List<TimedSystem>>();
        public const int TickSize = 1;
        private Schedule schedule;

        public Runner(Ecs ecs, Schedule schedule)
        {
            this.ecs = ecs;
            this.schedule = schedule;
            foreach (Frequency frequency in (Frequency[])Enum.GetValues(typeof(Frequency)))
            {
                systems[frequency] = new List<TimedSystem>();
            }
        }
        public void AddSystem(EntityComponentSystem.System system, Frequency frequency)
        {
            systems[frequency].Add(new TimedSystem() { Stopwatch = new Stopwatch(), System = system, Name = system.GetType().Name });
        }
        public void runFor(int years)
        {
            Stopwatch sw = new Stopwatch();
            for (var year = 0; year < years; year++)
            {
                sw.Restart();
                for (var tick = 0; tick < Ticks.PerYear; tick += TickSize)
                {
                    this.RunTick();
                }
                sw.Stop();
                var builder = new StringBuilder();
                builder.AppendLine($"{Environment.NewLine}Year {Ticks.ToParts(currentTick).Years}");
                builder.AppendLine($"- Ran in {sw.ElapsedMilliseconds} ms");
                // builder.AppendLine($"- Population: {entityPool.AliveEntities.Count()}");
                builder.AppendLine($"- Total Entities: { ecs.GetEntities().Count()}");
                builder.AppendLine($"- Systems:");
                foreach (TimedSystem system in systems.Values.SelectMany(systems => systems))
                {
                    builder.AppendLine($"  - {system.Name} ran in {system.Stopwatch.ElapsedMilliseconds}ms");
                    system.Stopwatch.Reset();
                }
                Console.WriteLine(builder.ToString());
            }
        }

        private void RunTick()
        {
            currentTick += TickSize;
            schedule.Run(currentTick);
            if (currentTick % Ticks.PerYear == 0)
            {
                RunSystems(this.systems[Frequency.Year]);
            }
            if (currentTick % Ticks.PerMonth == 0)
            {
                RunSystems(this.systems[Frequency.Month]);
            }
            if (currentTick % Ticks.PerDay == 0)
            {
                RunSystems(this.systems[Frequency.Day]);
            }
            if (currentTick % Ticks.PerHour == 0)
            {
                RunSystems(this.systems[Frequency.Hour]);
            }
            if (currentTick % Ticks.PerMinute == 0)
            {
                RunSystems(this.systems[Frequency.Minute]);
            }
            RunSystems(this.systems[Frequency.Tick]);
        }
        private void RunSystems(List<TimedSystem> timedSystems)
        {
            foreach (TimedSystem timedSystem in timedSystems)
            {
                timedSystem.Stopwatch.Start();
                timedSystem.System.Update(ecs, TickSize, currentTick);
                timedSystem.Stopwatch.Stop();
            }
        }

        public int currentTick { get; set; } = 0;

        private class TimedSystem
        {
            public EntityComponentSystem.System System;
            public Stopwatch Stopwatch;
            public string Name;
        }
    }
}