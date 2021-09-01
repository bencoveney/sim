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
                Console.WriteLine($"-----------------------");
                Console.WriteLine($"Year {Ticks.ToParts(CurrentTick).Years}");
                sw.Restart();
                for (var tick = 0; tick < Ticks.PerYear; tick += TickSize)
                {
                    RunTick();
                }
                sw.Stop();
                Console.WriteLine($"Ran in {sw.ElapsedMilliseconds} ms");
                // builder.AppendLine($"- Population: {entityPool.AliveEntities.Count()}");
                Console.WriteLine($"Total Entities: { ecs.GetEntities().Count()}");
                Console.WriteLine($"Systems:");
                foreach (TimedSystem system in systems.Values.SelectMany(systems => systems))
                {
                    Console.WriteLine($"- {system.Name} ran in {system.Stopwatch.ElapsedMilliseconds}ms");
                    system.Stopwatch.Reset();
                }
            }
        }

        private void RunTick()
        {
            CurrentTick += TickSize;
            schedule.Run(CurrentTick);
            if (CurrentTick % Ticks.PerYear == 0)
            {
                RunSystems(systems[Frequency.Year]);
            }
            if (CurrentTick % Ticks.PerMonth == 0)
            {
                RunSystems(systems[Frequency.Month]);
            }
            if (CurrentTick % Ticks.PerDay == 0)
            {
                RunSystems(systems[Frequency.Day]);
            }
            if (CurrentTick % Ticks.PerHour == 0)
            {
                RunSystems(systems[Frequency.Hour]);
            }
            if (CurrentTick % Ticks.PerMinute == 0)
            {
                RunSystems(systems[Frequency.Minute]);
            }
            RunSystems(systems[Frequency.Tick]);
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