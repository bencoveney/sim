using Sim.Ecs;

namespace Sim.Components
{
    public class HomeComponent : Component
    {
        private static uint kind = Component.kindGenerator.getNext();
        public override uint Kind { get { return kind; } }

        public uint EntityId;
        public HomeComponent(uint entityId)
        {
            EntityId = entityId;
        }

        public override string ToString()
        {
            return EntityId.ToString();
        }
    }
}