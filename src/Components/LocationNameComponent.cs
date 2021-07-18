using Sim.Ecs;

namespace Sim.Components
{
    public class LocationNameComponent : Component
    {
        private static uint kind = Component.kindGenerator.getNext();
        public override uint Kind { get { return kind; } }

        public string Name;
        public LocationNameComponent(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}