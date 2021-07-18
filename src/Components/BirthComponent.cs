using Sim.Ecs;
using Sim.Runner;

namespace Sim.Components
{
    public class BirthComponent : Component
    {
        private static uint kind = Component.kindGenerator.getNext();
        public override uint Kind { get { return kind; } }

        public int Tick;
        public BirthComponent(int tick)
        {
            Tick = tick;
        }

        public override string ToString()
        {
            return Ticks.ToDateString(Tick);
        }
    }
}