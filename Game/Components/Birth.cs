using EntityComponentSystem;
using Sim.Runner;

namespace Sim.Components
{
    [Component]
    public class Birth
    {
        public int Tick;
        public Birth(int tick)
        {
            Tick = tick;
        }

        public override string ToString()
        {
            return Ticks.ToDateString(Tick);
        }
    }
}