using sim.Render;
using EntityComponentSystem;
using Sim.Logging;
using Sim.Runner;
using Sim.Systems;
using Sim.Factories;

namespace Sim
{
    public class Program
    {
        private static void Main()
        {
            RunWorld();
        }

        private static void RunWorld()
        {
            var start = Ticks.From(50, 0, 0, 0, 0);

            var ecs = new Ecs();
            EcsExtensions.EcsExtensions.Register(ecs);

            var towns = 3;
            var townPop = 10;
            var popsize = towns * townPop;

            WorldFactory.Create(ecs, start, 3, townPop);

            Logger.LogEntities("Before running", ecs);

            var schedule = new Schedule();
            var runner = new Runner.Runner(ecs, schedule);
            runner.AddSystem(new DeathSystem(schedule), Frequency.Day);
            runner.AddSystem(new BirthSystem(popsize), Frequency.Day);
            runner.currentTick = start;
            runner.runFor(100);

            Logger.LogEntities("After running", ecs);
        }

        private static void RunRender()
        {
            // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (Game game = new Game(800, 600, "sim"))
            {
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                game.Run(60.0);
            }
        }
    }
}