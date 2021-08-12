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
        private readonly Ecs ecs;
        private readonly Dictionary<Frequency, List<TimedSystem>> systems = new();
        public const int TickSize = 1;
        private readonly Schedule schedule;

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
        public void RunFor(int years)
        {
            var sw = new Stopwatch();
            for (var year = 0; year < years; year++)
            {
                sw.Restart();
                for (var tick = 0; tick < Ticks.PerYear; tick += TickSize)
                {
                    this.RunTick();
                }
                sw.Stop();
                var builder = new StringBuilder();
                builder.AppendLine($"{Environment.NewLine}Year {Ticks.ToParts(CurrentTick).Years}");
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
            CurrentTick += TickSize;
            schedule.Run(CurrentTick);
            if (CurrentTick % Ticks.PerYear == 0)
            {
                RunSystems(this.systems[Frequency.Year]);
            }
            if (CurrentTick % Ticks.PerMonth == 0)
            {
                RunSystems(this.systems[Frequency.Month]);
            }
            if (CurrentTick % Ticks.PerDay == 0)
            {
                RunSystems(this.systems[Frequency.Day]);
            }
            if (CurrentTick % Ticks.PerHour == 0)
            {
                RunSystems(this.systems[Frequency.Hour]);
            }
            if (CurrentTick % Ticks.PerMinute == 0)
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
                timedSystem.System.Update(ecs, TickSize, CurrentTick);
                timedSystem.Stopwatch.Stop();
            }
        }

        public int CurrentTick { get; set; } = 0;

        private class TimedSystem
        {
            public EntityComponentSystem.System System;
            public Stopwatch Stopwatch;
            public string Name;
        }
    }
}