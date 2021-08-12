using EntityComponentSystem;
using Sim.Runner;

namespace Sim.Components
{
    [Component]
    public class Death
    {
        public int Tick;
        public Death(int tick)
        {
            Tick = tick;
        }

        public override string ToString()
        {
            return Ticks.ToDateString(Tick);
        }
    }
}